﻿using System;
using System.Threading;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Mvvm;
using TopDriveSystem.Commands.RtuModbus.Telemetry03;
using TopDriveSystem.ConfigApp.LookedLikeAbb.Parameters.ParameterDoubleReadonly;
using TopDriveSystem.ControlApp.Models.CommandSenderHost;
using TopDriveSystem.ControlApp.Models.Cycle;
using TopDriveSystem.ControlApp.Models.ParamLogger;
using TopDriveSystem.ControlApp.Models.TargetAddressHost;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb
{
    internal class Group03ParametersViewModel : ViewModelBase, ICyclePart
    {
        private readonly ICommandSenderHost _commandSenderHost;
        private readonly ILogger _logger;

        private readonly object _syncCancel;
        private readonly ITargetAddressHost _targerAddressHost;
        private readonly IUserInterfaceRoot _uiRoot;
        private bool _cancel;
        private int _errorCounts;
        private bool _readingInProgress;

        public Group03ParametersViewModel(ICommandSenderHost commandSenderHost, ITargetAddressHost targerAddressHost,
            IUserInterfaceRoot uiRoot, ILogger logger, IParameterLogger parameterLogger)
        {
            _commandSenderHost = commandSenderHost;
            _targerAddressHost = targerAddressHost;
            _uiRoot = uiRoot;
            _logger = logger;

            Parameter01Vm =
                new ParameterDoubleReadonlyViewModel("03.01. Коэффициент модуляции ШИМ [%]", "f0", null,
                    parameterLogger);
            Parameter02Vm =
                new ParameterDoubleReadonlyViewModel("03.02. Выход регулятора тока D [%]", "f0", null, parameterLogger);
            Parameter03Vm =
                new ParameterDoubleReadonlyViewModel("03.03. Выход регулятора тока Q [%]", "f0", null, parameterLogger);

            Parameter04Vm = new ParameterDoubleReadonlyViewModel("03.04. Измеренная составляющая тока D [%]", "f0",
                null, parameterLogger);
            Parameter05Vm = new ParameterDoubleReadonlyViewModel("03.05. Измеренная составляющая тока Q [%]", "f0",
                null, parameterLogger);
            Parameter06Vm = new ParameterDoubleReadonlyViewModel(
                "03.06. Выход регулятора компенсатора вычислителя потока D [В]", "f0", null, parameterLogger);
            Parameter07Vm = new ParameterDoubleReadonlyViewModel(
                "03.07. Выход регулятора компенсатора вычислителя потока Q [В]", "f0", null, parameterLogger);

            Parameter08Vm = new ParameterDoubleReadonlyViewModel("03.08. Вспомогательная ячейка №1 АИН1", "f0", null,
                parameterLogger);
            Parameter09Vm = new ParameterDoubleReadonlyViewModel("03.09. Вспомогательная ячейка №2 АИН1", "f0", null,
                parameterLogger);

            Parameter10Vm = new ParameterDoubleReadonlyViewModel(
                "03.10. Вычисленное текущее значение теплового показателя двигателя [А^2*c]", "f0", null,
                parameterLogger);
            Parameter11Vm = new ParameterDoubleReadonlyViewModel(
                "03.11. (Ведомый привод) Уставка моментного тока (Выход регулятора скорости) [%]", "f0", null,
                parameterLogger);

            ReadCycleCmd = new RelayCommand(ReadCycleFunc, () => !_readingInProgress); // TODO: check port opened
            StopReadCycleCmd = new RelayCommand(StopReadingFunc, () => _readingInProgress);

            _syncCancel = new object();
            _cancel = true;
            _readingInProgress = false;
            _errorCounts = 0;
        }

        public ParameterDoubleReadonlyViewModel Parameter01Vm { get; }
        public ParameterDoubleReadonlyViewModel Parameter02Vm { get; }
        public ParameterDoubleReadonlyViewModel Parameter03Vm { get; }
        public ParameterDoubleReadonlyViewModel Parameter04Vm { get; }
        public ParameterDoubleReadonlyViewModel Parameter05Vm { get; }
        public ParameterDoubleReadonlyViewModel Parameter06Vm { get; }
        public ParameterDoubleReadonlyViewModel Parameter07Vm { get; }
        public ParameterDoubleReadonlyViewModel Parameter08Vm { get; }
        public ParameterDoubleReadonlyViewModel Parameter09Vm { get; }
        public ParameterDoubleReadonlyViewModel Parameter10Vm { get; }
        public ParameterDoubleReadonlyViewModel Parameter11Vm { get; }

        public RelayCommand ReadCycleCmd { get; }
        public RelayCommand StopReadCycleCmd { get; }

        public void InCycleAction()
        {
            var waiter = new ManualResetEvent(false);
            var cmd = new ReadTelemetry03Command();
            _commandSenderHost.Sender.SendCommandAsync(_targerAddressHost.TargetAddress,
                cmd, TimeSpan.FromSeconds(0.1), 2,
                (exception, bytes) =>
                {
                    ITelemetry03 telemetry = null;
                    try
                    {
                        if (exception != null) throw new Exception("Произошла ошибка во время обмена", exception);
                        var result = cmd.GetResult(bytes);
                        _errorCounts = 0;
                        telemetry = result;
                    }
                    catch (Exception ex)
                    {
                        _errorCounts++; // TODO: потенциально опасная ситуация (переполнение инта (примерно через 233 часа при опросе телеметрии раз в 50 милисекунд)
                        telemetry = null;
                        //_logger.Log("Ошибка: " + ex.Message);
                        //Console.WriteLine(ex);
                    }
                    finally
                    {
                        _uiRoot.Notifier.Notify(() => { UpdateTelemetry(telemetry); });
                        waiter.Set();
                    }
                });
            waiter.WaitOne();
            waiter.Reset();
        }

        public bool Cancel
        {
            get
            {
                lock (_syncCancel)
                {
                    return _cancel;
                }
            }
            set
            {
                lock (_syncCancel)
                {
                    _cancel = value;
                }
            }
        }


        private void StopReadingFunc()
        {
            Cancel = true;
            _readingInProgress = false;

            _logger.Log("Взведен внутренний флаг прерывания циклического опроса");
            ReadCycleCmd.RaiseCanExecuteChanged();
            StopReadCycleCmd.RaiseCanExecuteChanged();
        }

        private void ReadCycleFunc()
        {
            _logger.Log("Запуск циклического опроса телеметрии");
            Cancel = false;

            _readingInProgress = true;
            ReadCycleCmd.RaiseCanExecuteChanged();
            StopReadCycleCmd.RaiseCanExecuteChanged();
        }

        private void UpdateTelemetry(ITelemetry03 telemetry)
        {
            const int maxErrors = 3;
            if (telemetry == null && _errorCounts < maxErrors) return;

            Parameter01Vm.CurrentValue = telemetry?.Kpwm;
            Parameter02Vm.CurrentValue = telemetry?.Ud;

            Parameter03Vm.CurrentValue = telemetry?.Uq;
            Parameter04Vm.CurrentValue = telemetry?.Id;
            Parameter05Vm.CurrentValue = telemetry?.Iq;

            Parameter06Vm.CurrentValue = telemetry?.UcompD;
            Parameter07Vm.CurrentValue = telemetry?.UCompQ;

            Parameter08Vm.CurrentValue = telemetry?.Aux1;
            Parameter09Vm.CurrentValue = telemetry?.Aux2;
            Parameter10Vm.CurrentValue = telemetry?.I2t;
            Parameter11Vm.CurrentValue = telemetry?.FollowMout;
        }
    }
}