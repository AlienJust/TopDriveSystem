using System;
using System.Threading;
using System.Windows.Input;
using AlienJust.Support.Concurrent;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Mvvm;
using AlienJust.Support.UI.Contracts;
using TopDriveSystem.Commands.Cooler;
using TopDriveSystem.ConfigApp.AppControl.CommandSenderHost;
using TopDriveSystem.ConfigApp.AppControl.TargetAddressHost;

namespace TopDriveSystem.ConfigApp.CoolerTelemetry
{
    internal class CoolerTelemetriesViewModel : ViewModelBase
    {
        private readonly IWorker<Action> _backWorker;
        private readonly ICommandSenderHost _commandSenderHost;

        private readonly IMultiLoggerWithStackTrace<int> _debugLogger;
        private readonly ILogger _logger;

        private readonly RelayCommand _readCycleCommand;
        private readonly RelayCommand _stopReadingCommand;

        private readonly object _syncCancel;
        private readonly ITargetAddressHost _targerAddressHost;
        private readonly IUserInterfaceRoot _userInterfaceRoot;
        private readonly IWindowSystem _windowSystem;
        private bool _cancel;

        private bool _readingInProgress;


        public CoolerTelemetriesViewModel(ICommandSenderHost commandSenderHost, ITargetAddressHost targerAddressHost,
            IUserInterfaceRoot userInterfaceRoot, ILogger logger, IMultiLoggerWithStackTrace<int> debugLogger,
            IWindowSystem windowSystem)
        {
            _commandSenderHost = commandSenderHost;
            _targerAddressHost = targerAddressHost;
            _userInterfaceRoot = userInterfaceRoot;
            _logger = logger;
            _debugLogger = debugLogger;
            _windowSystem = windowSystem;

            _readCycleCommand = new RelayCommand(ReadCycle, () => !_readingInProgress);
            _stopReadingCommand = new RelayCommand(StopReading, () => _readingInProgress);

            CoolerTelemetryVm = new CoolerTelemetryViewModel();

            _backWorker = new SingleThreadedRelayQueueWorker<Action>("CoolerTelemetryBackWorker", a => a(),
                ThreadPriority.BelowNormal, true, null, _debugLogger.GetLogger(0));
            _syncCancel = new object();
            _cancel = false;
            _readingInProgress = false;
        }

        public CoolerTelemetryViewModel CoolerTelemetryVm { get; }

        public ICommand ReadCycleCommand => _readCycleCommand;

        public ICommand StopReadingCommand => _stopReadingCommand;

        private bool Cancel
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

        private void StopReading()
        {
            Cancel = true;
            _logger.Log("Взведен внутренний флаг прерывания циклического опроса");
        }

        private void ReadCycle()
        {
            _logger.Log("Запуск циклического опроса телеметрии");
            Cancel = false;
            _readingInProgress = true;
            _readCycleCommand.RaiseCanExecuteChanged();
            _stopReadingCommand.RaiseCanExecuteChanged();


            _backWorker.AddWork(() =>
            {
                try
                {
                    var w8er = new ManualResetEvent(false);
                    while (!Cancel)
                    {
                        var cmd = new ReadCoolerTelemetryCommand();
                        _commandSenderHost.Sender.SendCommandAsync(
                            0x01,
                            cmd,
                            TimeSpan.FromSeconds(0.2), 2,
                            (exception, bytes) =>
                            {
                                ICoolerTelemetry coolerTelemetry = null;
                                try
                                {
                                    if (exception != null)
                                        throw new Exception("Произошла ошибка во время обмена", exception);
                                    var result = cmd.GetResult(bytes);
                                    coolerTelemetry = result;
                                }
                                catch (Exception ex)
                                {
                                    // TODO: log exception, null values
                                    _logger.Log("Ошибка: " + ex.Message);
                                    Console.WriteLine(ex);
                                }
                                finally
                                {
                                    _userInterfaceRoot.Notifier.Notify(() =>
                                        CoolerTelemetryVm.UpdateTelemetry(coolerTelemetry));
                                    w8er.Set();
                                }
                            });
                        w8er.WaitOne();
                        w8er.Reset();
                        Thread.Sleep(300); // TODO: interval must be setted by user
                    }
                }
                catch (Exception ex)
                {
                    _logger.Log("Ошибка фонового потока очереди отправки: " + ex.Message);
                }
                finally
                {
                    _logger.Log("Циклический опрос окончен");
                    _userInterfaceRoot.Notifier.Notify(() =>
                    {
                        _readingInProgress = false;
                        _readCycleCommand.RaiseCanExecuteChanged();
                        _stopReadingCommand.RaiseCanExecuteChanged();
                    });
                }
            });
        }
    }
}