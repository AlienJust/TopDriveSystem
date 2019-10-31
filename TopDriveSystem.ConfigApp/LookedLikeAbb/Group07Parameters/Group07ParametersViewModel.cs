using System;
using System.Threading;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Mvvm;
using TopDriveSystem.Commands.RtuModbus.Telemetry07;
using TopDriveSystem.ConfigApp.AppControl.CommandSenderHost;
using TopDriveSystem.ConfigApp.AppControl.Cycle;
using TopDriveSystem.ConfigApp.AppControl.ParamLogger;
using TopDriveSystem.ConfigApp.AppControl.TargetAddressHost;
using TopDriveSystem.ConfigApp.LookedLikeAbb.Group07Parameters.McwParameter;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.Group07Parameters
{
    internal class Group07ParametersViewModel : ViewModelBase, ICyclePart
    {
        private readonly ICommandSenderHost _commandSenderHost;
        private readonly ILogger _logger;

        private readonly object _syncCancel;
        private readonly ITargetAddressHost _targerAddressHost;
        private readonly IUserInterfaceRoot _uiRoot;
        private bool _cancel;
        private int _errorCounts;
        private bool _readingInProgress;

        public Group07ParametersViewModel(ICommandSenderHost commandSenderHost, ITargetAddressHost targerAddressHost,
            IUserInterfaceRoot uiRoot, ILogger logger, IParameterLogger parameterLogger)
        {
            _commandSenderHost = commandSenderHost;
            _targerAddressHost = targerAddressHost;
            _uiRoot = uiRoot;
            _logger = logger;

            Parameter01Vm = new McwParameterViewModel(parameterLogger);


            ReadCycleCmd = new RelayCommand(ReadCycleFunc, () => !_readingInProgress); // TODO: check port opened
            StopReadCycleCmd = new RelayCommand(StopReadingFunc, () => _readingInProgress);

            _syncCancel = new object();
            _cancel = true;
            _readingInProgress = false;
            _errorCounts = 0;
        }

        public McwParameterViewModel Parameter01Vm { get; }

        public RelayCommand ReadCycleCmd { get; }
        public RelayCommand StopReadCycleCmd { get; }

        public void InCycleAction()
        {
            var waiter = new ManualResetEvent(false);
            var cmd = new ReadTelemetry07Command();
            _commandSenderHost.Sender.SendCommandAsync(_targerAddressHost.TargetAddress,
                cmd, TimeSpan.FromSeconds(0.1), 2,
                (exception, bytes) =>
                {
                    ITelemetry07 telemetry = null;
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

        private void UpdateTelemetry(ITelemetry07 telemetry)
        {
            const int maxErrors = 3; // TODO: extract common constant for all telemetry updateables
            if (telemetry == null && _errorCounts < maxErrors)
                return;
            Parameter01Vm.UpdateTelemetry(telemetry?.Mcw);
        }
    }
}