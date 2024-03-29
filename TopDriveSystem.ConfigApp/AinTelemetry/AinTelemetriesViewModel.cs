﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Mvvm;
using AlienJust.Support.UI.Contracts;
using TopDriveSystem.Commands.SystemControl;
using TopDriveSystem.ControlApp.Models.CommandSenderHost;
using TopDriveSystem.ControlApp.Models.Cycle;
using TopDriveSystem.ControlApp.Models.TargetAddressHost;

namespace TopDriveSystem.ConfigApp.AinTelemetry
{
    internal class AinTelemetriesViewModel : ViewModelBase, ICommonAinTelemetryVm, IAinTelemetriesCycleControl,
        ICyclePart
    {
        private readonly List<AinTelemetryExpandedViewModel> _ainTelemetryVms;
        private readonly ICommandSenderHost _commandSenderHost;

        private readonly IDebugInformationShower _debugInformationShower;
        private readonly ILogger _logger;

        private readonly RelayCommand _readCycleCommand;
        private readonly RelayCommand _stopReadingCommand;

        private readonly object _syncCancel;
        private readonly ITargetAddressHost _targerAddressHost;
        private readonly IUserInterfaceRoot _userInterfaceRoot;
        private readonly IWindowSystem _windowSystem;
        private bool _cancel;

        private bool _readingInProgress;

        public AinTelemetriesViewModel(ICommandSenderHost commandSenderHost, ITargetAddressHost targerAddressHost,
            IUserInterfaceRoot userInterfaceRoot, ILogger logger, IWindowSystem windowSystem,
            IDebugInformationShower debugInformationShower, TelemetryCommonViewModel externalTelemetryVm,
            AinTelemetryViewModel ain1TelemetyVm, AinTelemetryViewModel ain2TelemetyVm,
            AinTelemetryViewModel ain3TelemetyVm)
        {
            _commandSenderHost = commandSenderHost;
            _targerAddressHost = targerAddressHost;
            _userInterfaceRoot = userInterfaceRoot;
            _logger = logger;
            _windowSystem = windowSystem;
            _debugInformationShower = debugInformationShower;

            CommonTelemetryVm = externalTelemetryVm;

            _readCycleCommand = new RelayCommand(ReadCycle, () => !_readingInProgress); // TODO: check port opened
            _stopReadingCommand = new RelayCommand(StopReading, () => _readingInProgress);

            _ainTelemetryVms = new List<AinTelemetryExpandedViewModel>
            {
                new AinTelemetryExpandedViewModel("АИН №1", ain1TelemetyVm),
                new AinTelemetryExpandedViewModel("АИН №2", ain2TelemetyVm),
                new AinTelemetryExpandedViewModel("АИН №3", ain3TelemetyVm)
            };


            //_backWorker = new SingleThreadedRelayQueueWorker<Action>("AinTelemetryBackWorker", a => a(), ThreadPriority.BelowNormal, true, null, new RelayActionLogger(Console.WriteLine, new ChainedFormatter(new List<ITextFormatter> {new PreffixTextFormatter("TelemetryBackWorker > "), new DateTimeFormatter(" > ")})));
            _syncCancel = new object();
            _cancel = true;
            _readingInProgress = false;
        }

        public IEnumerable<AinTelemetryExpandedViewModel> AinTelemetryVms => _ainTelemetryVms;

        public TelemetryCommonViewModel CommonTelemetryVm { get; }

        public ICommand ReadCycleCommand => _readCycleCommand;

        public ICommand StopReadingCommand => _stopReadingCommand;

        public void UpdateCommonEngineState(ushort? value)
        {
            CommonTelemetryVm.UpdateCommonEngineState(value);
        }

        public void UpdateCommonFaultState(ushort? value)
        {
            CommonTelemetryVm.UpdateCommonFaultState(value);
        }

        public void UpdateAinsLinkState(bool? ain1LinkFault, bool? ain2LinkFault, bool? ain3LinkFault)
        {
            CommonTelemetryVm.UpdateAinsLinkState(ain1LinkFault, ain2LinkFault, ain3LinkFault);
        }

        public void UpdateAinStatuses(ushort? status1, ushort? status2, ushort? status3)
        {
            CommonTelemetryVm.UpdateAinStatuses(status1, status2, status3);
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


        public void InCycleAction()
        {
            var waiter = new ManualResetEvent(false);

            var cmdDebug = new ReadDebugInfoCommand();
            _commandSenderHost.Sender.SendCommandAsync(0x01, cmdDebug, TimeSpan.FromSeconds(0.1), 2,
                (exception, bytes) =>
                {
                    try
                    {
                        if (exception != null) throw new Exception("Произошла ошибка во время обмена", exception);
                        _userInterfaceRoot.Notifier.Notify(() => _debugInformationShower.ShowBytes(bytes));
                    }
                    catch (Exception ex)
                    {
                        // TODO: no need to log to reduce log spam
                        //_logger.Log("Ошибка: " + ex.Message);
                        //Console.WriteLine(ex);
                    }

                    waiter.Set();
                });
            waiter.WaitOne();
            waiter.Reset();
        }

        private void StopReading()
        {
            Cancel = true;
            foreach (var ainTelemetryExpandedViewModel in _ainTelemetryVms)
                ainTelemetryExpandedViewModel.AinTelemetryVm.Cancel = true;

            _readingInProgress = false;
            _logger.Log("Взведен внутренний флаг прерывания циклического опроса");
            _readCycleCommand.RaiseCanExecuteChanged();
            _stopReadingCommand.RaiseCanExecuteChanged();
        }

        private void ReadCycle()
        {
            _logger.Log("Запуск циклического опроса телеметрии");
            Cancel = false;
            foreach (var ainTelemetryExpandedViewModel in _ainTelemetryVms)
                ainTelemetryExpandedViewModel.AinTelemetryVm.Cancel = false;
            _readingInProgress = true;
            _readCycleCommand.RaiseCanExecuteChanged();
            _stopReadingCommand.RaiseCanExecuteChanged();
        }
    }
}