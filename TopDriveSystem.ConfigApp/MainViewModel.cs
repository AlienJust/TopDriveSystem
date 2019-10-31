using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Media;
using AlienJust.Support.Concurrent;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Mvvm;
using AlienJust.Support.Text;
using AlienJust.Support.UI.Contracts;
using TopDriveSystem.CommandSenders.SerialPortBased;
using TopDriveSystem.CommandSenders.TestCommandSender;
using TopDriveSystem.ConfigApp.AinCommand;
using TopDriveSystem.ConfigApp.AinTelemetry;
using TopDriveSystem.ConfigApp.AppControl.AinsCounter;
using TopDriveSystem.ConfigApp.AppControl.AinSettingsRead;
using TopDriveSystem.ConfigApp.AppControl.AinSettingsStorage;
using TopDriveSystem.ConfigApp.AppControl.AinSettingsWrite;
using TopDriveSystem.ConfigApp.AppControl.CommandSenderHost;
using TopDriveSystem.ConfigApp.AppControl.Cycle;
using TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace;
using TopDriveSystem.ConfigApp.AppControl.LoggerHost;
using TopDriveSystem.ConfigApp.AppControl.NotifySendingEnabled;
using TopDriveSystem.ConfigApp.AppControl.ParamLogger;
using TopDriveSystem.ConfigApp.AppControl.TargetAddressHost;
using TopDriveSystem.ConfigApp.BsEthernetLogs;
using TopDriveSystem.ConfigApp.EngineAutoSetup;
using TopDriveSystem.ConfigApp.Logs;
using TopDriveSystem.ConfigApp.LookedLikeAbb.AinSettingsRw;
using TopDriveSystem.ConfigApp.MnemonicCheme;
using TopDriveSystem.ConfigApp.NewLook.Archive;
using TopDriveSystem.ConfigApp.NewLook.OldLook;
using TopDriveSystem.ConfigApp.NewLook.Settings;
using TopDriveSystem.ConfigApp.NewLook.Telemetry;
using Colors = AlienJust.Support.Wpf.Converters.Colors;
using IAinSettingsReadNotifyRaisable =
    TopDriveSystem.ConfigApp.AppControl.AinSettingsRead.IAinSettingsReadNotifyRaisable;

namespace TopDriveSystem.ConfigApp
{
    internal class MainViewModel : ViewModelBase, ILinkContol
    {
        private const string TestComPortName = "ТЕСТ";
        private readonly IAinsCounterRaisable _ainsCounterRaisable;
        private readonly IAinSettingsReader _ainSettingsReader;
        private readonly IAinSettingsReadNotify _ainSettingsReadNotify;
        private readonly IAinSettingsWriter _ainSettingsWriter;


        private readonly AutoSettingsReader _autoSettingsReader;
        public readonly List<Color> _colors;
        private readonly ICommandSenderHost _commandSenderHost;

        private readonly ICommandSenderHostSettable _commandSenderHostSettable;
        private readonly ICycleThreadHolder _cycleThreadHolder;
        private readonly IMultiLoggerWithStackTrace<int> _debugLogger;

        private readonly IEngineSettingsReader _engineSettingsReader;
        private readonly IEngineSettingsReadNotify _engineSettingsReadNotify;
        private readonly IEngineSettingsReadNotifyRaisable _engineSettingsReadNotifyRaisable;
        private readonly IEngineSettingsStorage _engineSettingsStorage;
        private readonly IEngineSettingsStorageSettable _engineSettingsStorageSettable;
        private readonly IEngineSettingsStorageUpdatedNotify _engineSettingsStorageUpdatedNotify;
        private readonly IEngineSettingsWriter _engineSettingsWriter;

        private readonly ILogger _logger;
        private readonly ILoggerRegistrationPoint _loggerRegistrationPoint;
        private readonly INotifySendingEnabledRaisable _notifySendingEnabled;


        private readonly IParameterLogger _paramLogger;

        private readonly ITargetAddressHost _targetAddressHost;

        private readonly IUserInterfaceRoot _uiRoot;
        private bool _ain1IsUsed;
        private Colors _ain1StateColor;
        private bool _ain2IsUsed;
        private Colors _ain2StateColor;
        private bool _ain3IsUsed;
        private Colors _ain3StateColor;

        private AutoTimeSetter _autoTimeSetter;

        private List<string> _comPortsAvailable;

        private bool _isPortOpened;
        private string _selectedComName;

        public MainViewModel(IUserInterfaceRoot uiRoot, IWindowSystem windowSystem, List<Color> colors,
            ICommandSenderHostSettable commandSenderHostSettable, ITargetAddressHost targetAddressHost,
            IMultiLoggerWithStackTrace<int> debugLogger, ILoggerRegistrationPoint loggerRegistrationPoint,
            INotifySendingEnabledRaisable notifySendingEnabled, IParameterLogger paramLogger,
            IAinsCounterRaisable ainsCounterRaisable,
            ICycleThreadHolder cycleThreadHolder,
            IAinSettingsReader ainSettingsReader, IAinSettingsReadNotify ainSettingsReadNotify,
            IAinSettingsReadNotifyRaisable ainSettingsReadNotifyRaisable, IAinSettingsWriter ainSettingsWriter,
            IAinSettingsStorage ainSettingsStorage, IAinSettingsStorageSettable ainSettingsStorageSettable,
            IAinSettingsStorageUpdatedNotify storageUpdatedNotify,
            ReadCycleModel bsEthernetReadCycleModel,
            IEngineSettingsReader engineSettingsReader,
            IEngineSettingsWriter engineSettingsWriter,
            IEngineSettingsReadNotify engineSettingsReadNotify,
            IEngineSettingsReadNotifyRaisable engineSettingsReadNotifyRaisable,
            IEngineSettingsStorage engineSettingsStorage,
            IEngineSettingsStorageSettable engineSettingsStorageSettable,
            IEngineSettingsStorageUpdatedNotify engineSettingsStorageUpdatedNotify)
        {
            _uiRoot = uiRoot;
            _colors = colors;

            _commandSenderHostSettable = commandSenderHostSettable;
            _commandSenderHost = commandSenderHostSettable;
            _targetAddressHost = targetAddressHost;

            _isPortOpened = false;

            // Лог программы:
            _debugLogger = debugLogger;
            _loggerRegistrationPoint = loggerRegistrationPoint;

            // разрешение к отправке (COM-порт открыт/закрыт)
            _notifySendingEnabled = notifySendingEnabled;

            ProgramLogVm = new ProgramLogViewModel(_uiRoot, _debugLogger, new DateTimeFormatter(" > "));
            _logger = new RelayLogger(ProgramLogVm);
            _loggerRegistrationPoint.RegisterLoggegr(_logger);

            GetPortsAvailable();

            OpenPortCommand = new RelayCommand(OpenPort, () => !_isPortOpened);
            ClosePortCommand = new RelayCommand(ClosePort, () => _isPortOpened);
            GetPortsAvailableCommand = new RelayCommand(GetPortsAvailable);

            _paramLogger = paramLogger;


            _ainsCounterRaisable = ainsCounterRaisable;
            _cycleThreadHolder = cycleThreadHolder;
            _ainSettingsReader = ainSettingsReader;
            _ainSettingsReadNotify = ainSettingsReadNotify;
            _ainSettingsWriter = ainSettingsWriter;

            // Блоки АИН в системе:
            AinsCountInSystem = new List<int> {1, 2, 3};
            SelectedAinsCount = AinsCountInSystem.First();

            var ainSettingsReadedWriter = new AinSettingsReaderWriter(_ainSettingsReader, _ainSettingsWriter);

            _engineSettingsReader = engineSettingsReader;
            _engineSettingsWriter = engineSettingsWriter;
            _engineSettingsReadNotify = engineSettingsReadNotify;
            _engineSettingsReadNotifyRaisable = engineSettingsReadNotifyRaisable;
            _engineSettingsStorage = engineSettingsStorage;
            _engineSettingsStorageSettable = engineSettingsStorageSettable;
            _engineSettingsStorageUpdatedNotify = engineSettingsStorageUpdatedNotify;


            AinCommandAndCommonTelemetryVm = new AinCommandAndCommonTelemetryViewModel(
                new AinCommandAndMinimalCommonTelemetryViewModel(_commandSenderHost, _targetAddressHost, _uiRoot,
                    _logger, _notifySendingEnabled, 0, ainSettingsStorage, storageUpdatedNotify),
                new TelemetryCommonViewModel(), _commandSenderHost, _targetAddressHost, _uiRoot, _notifySendingEnabled);

            _cycleThreadHolder.RegisterAsCyclePart(AinCommandAndCommonTelemetryVm);

            TelemtryVm = new TelemetryViewModel(_uiRoot, _commandSenderHost, _targetAddressHost, _logger,
                _cycleThreadHolder, _ainsCounterRaisable, _paramLogger, _notifySendingEnabled);

            SettingsVm = new SettingsViewModel(_uiRoot, _logger,
                ainSettingsReadedWriter, _ainSettingsReadNotify, ainSettingsReadNotifyRaisable, ainSettingsStorage,
                ainSettingsStorageSettable, storageUpdatedNotify, _ainsCounterRaisable,
                _commandSenderHost, _targetAddressHost, _notifySendingEnabled,
                _engineSettingsReader,
                _engineSettingsWriter,
                _engineSettingsReadNotify,
                _engineSettingsReadNotifyRaisable,
                _engineSettingsStorage,
                _engineSettingsStorageSettable,
                _engineSettingsStorageUpdatedNotify,
                _debugLogger); // TODO: can be moved to app.xaml.cs if needed

            ArchiveVm = new ArchivesViewModel(
                new ArchiveViewModel(_commandSenderHost, _targetAddressHost, _uiRoot, _logger, _notifySendingEnabled,
                    0),
                new ArchiveViewModel(_commandSenderHost, _targetAddressHost, _uiRoot, _logger, _notifySendingEnabled,
                    1));

            MnemonicChemeVm =
                new MnemonicChemeViewModel(Path.Combine(Environment.CurrentDirectory, "mnemoniccheme.png"));
            OldLookVm = new OldLookViewModel(_uiRoot, windowSystem, _commandSenderHost, _targetAddressHost,
                _notifySendingEnabled, this, _logger, _debugLogger, _cycleThreadHolder, _ainsCounterRaisable,
                _paramLogger, ainSettingsStorage, storageUpdatedNotify);

            _ain1StateColor = Colors.Gray;
            _ain2StateColor = Colors.Gray;
            _ain3StateColor = Colors.Gray;

            _ain1IsUsed = true;
            _ain2IsUsed = false;
            _ain3IsUsed = false;

            _ainsCounterRaisable.AinsCountInSystemHasBeenChanged += ainsCount =>
            {
                switch (ainsCount)
                {
                    case 1:
                        Ain1IsUsed = true;
                        Ain2IsUsed = false;
                        Ain3IsUsed = false;
                        break;
                    case 2:
                        Ain1IsUsed = true;
                        Ain2IsUsed = true;
                        Ain3IsUsed = false;
                        break;
                    case 3:
                        Ain1IsUsed = true;
                        Ain2IsUsed = true;
                        Ain3IsUsed = true;
                        break;
                    default:
                        throw new Exception("Такое число АИН в системе не поддерживается");
                }
            };

            AinCommandAndCommonTelemetryVm.AinsLinkInformationHasBeenUpdated += (ain1Error, ain2Error, ain3Error) =>
            {
                Ain1StateColor = ain1Error.HasValue ? ain1Error.Value ? Colors.Red : Colors.YellowGreen : Colors.Gray;
                Ain2StateColor = ain2Error.HasValue ? ain2Error.Value ? Colors.Red : Colors.YellowGreen : Colors.Gray;
                Ain3StateColor = ain3Error.HasValue ? ain3Error.Value ? Colors.Red : Colors.YellowGreen : Colors.Gray;
            };

            _notifySendingEnabled.SendingEnabledChanged += isEnabled =>
            {
                // TODO: execution in ui thread
                Ain1StateColor = Colors.Gray;
                Ain2StateColor = Colors.Gray;
                Ain3StateColor = Colors.Gray;
            };

            EngineAutoSetupVm = new EngineAutoSetupViewModel(
                new TableViewModel("Начальные значения:", _logger),
                new TableViewModel("После тестирования:", _logger),
                _notifySendingEnabled, _ainSettingsReader, _ainSettingsReadNotify, _ainSettingsWriter, _uiRoot, _logger,
                _commandSenderHost, _targetAddressHost, bsEthernetReadCycleModel);

            _logger.Log("Программа загружена");
        }

        public IParameterLogger ExternalParamLogger { get; set; }

        public AinCommandAndCommonTelemetryViewModel AinCommandAndCommonTelemetryVm { get; }


        public MnemonicChemeViewModel MnemonicChemeVm { get; }
        public OldLookViewModel OldLookVm { get; }
        public ArchivesViewModel ArchiveVm { get; }
        public SettingsViewModel SettingsVm { get; }
        public TelemetryViewModel TelemtryVm { get; }
        public EngineAutoSetupViewModel EngineAutoSetupVm { get; }

        public List<string> ComPortsAvailable
        {
            get => _comPortsAvailable;
            set
            {
                if (_comPortsAvailable != value)
                {
                    _comPortsAvailable = value;
                    RaisePropertyChanged(() => ComPortsAvailable);
                }
            }
        }

        public string SelectedComName
        {
            get => _selectedComName;
            set
            {
                if (value != _selectedComName)
                {
                    _selectedComName = value;
                    RaisePropertyChanged(() => SelectedComName);
                }
            }
        }

        public RelayCommand OpenPortCommand { get; }

        public RelayCommand ClosePortCommand { get; }

        public RelayCommand GetPortsAvailableCommand { get; }

        public ProgramLogViewModel ProgramLogVm { get; }


        public List<int> AinsCountInSystem { get; }

        public int SelectedAinsCount
        {
            get => _ainsCounterRaisable.SelectedAinsCount;
            set
            {
                if (value != 1 && value != 2 && value != 3)
                    throw new ArgumentOutOfRangeException(
                        "Поддерживаемое число блоков АИН в системе может быть только 1, 2 или 3, получено ошибочное число: " +
                        value);
                _ainsCounterRaisable.SetAinsCountAndRaiseChange(value);
                RaisePropertyChanged(() => SelectedAinsCount);
            }
        }

        public Colors Ain1StateColor
        {
            get => _ain1StateColor;
            set
            {
                if (_ain1StateColor != value)
                {
                    _ain1StateColor = value;
                    RaisePropertyChanged(() => Ain1StateColor);
                }
            }
        }

        public Colors Ain2StateColor
        {
            get => _ain2StateColor;
            set
            {
                if (_ain2StateColor != value)
                {
                    _ain2StateColor = value;
                    RaisePropertyChanged(() => Ain2StateColor);
                }
            }
        }

        public Colors Ain3StateColor
        {
            get => _ain3StateColor;
            set
            {
                if (_ain3StateColor != value)
                {
                    _ain3StateColor = value;
                    RaisePropertyChanged(() => Ain3StateColor);
                }
            }
        }

        public bool Ain1IsUsed
        {
            get => _ain1IsUsed;
            set
            {
                if (_ain1IsUsed != value)
                {
                    _ain1IsUsed = value;
                    RaisePropertyChanged(() => Ain1IsUsed);
                }
            }
        }

        public bool Ain2IsUsed
        {
            get => _ain2IsUsed;
            set
            {
                if (_ain2IsUsed != value)
                {
                    _ain2IsUsed = value;
                    RaisePropertyChanged(() => Ain2IsUsed);
                }
            }
        }

        public bool Ain3IsUsed
        {
            get => _ain3IsUsed;
            set
            {
                if (_ain3IsUsed != value)
                {
                    _ain3IsUsed = value;
                    RaisePropertyChanged(() => Ain3IsUsed);
                }
            }
        }

        public void CloseComPort()
        {
            ClosePort();
        }

        private void ClosePort()
        {
            try
            {
                _notifySendingEnabled.SetIsSendingEnabledAndRaiseChange(false);
                var currentSender = _commandSenderHost.Sender;
                _logger.Log("Закрытие ранее открытого порта " + currentSender + "...");

                // Вызов SilentSender.EndWork не производится!
                currentSender.Dispose();
                _commandSenderHostSettable.SetCommandSender(null);

                _isPortOpened = false;
                OpenPortCommand.RaiseCanExecuteChanged();
                ClosePortCommand.RaiseCanExecuteChanged();
                _logger.Log("Ранее открытый порт " + currentSender + " закрыт");
            }
            catch (Exception ex)
            {
                _logger.Log("Не удалось закрыть открытый ранее порт. " + ex.Message);
            }
        }

        private void OpenPort()
        {
            // must be called only from UI
            try
            {
                if (_isPortOpened) ClosePort();
                _logger.Log("Открытие порта " + _selectedComName + "...");

                if (_selectedComName == TestComPortName)
                {
                    var backWorker =
                        new SingleThreadedRelayQueueWorkerProceedAllItemsBeforeStopNoLog<Action>("NbBackWorker",
                            a => a(), ThreadPriority.BelowNormal, true, null);
                    var sender =
                        new NothingBasedCommandSender(backWorker, backWorker /*, _debugLogger, _uiRoot.Notifier*/);
                    _commandSenderHostSettable.SetCommandSender(sender);
                }
                else
                {
                    var backWorker =
                        new SingleThreadedRelayQueueWorkerProceedAllItemsBeforeStopNoLog<Action>("SerialPortBackWorker",
                            a => a(), ThreadPriority.BelowNormal, true, null);
                    var sender =
                        new SerialPortBasedCommandSender(backWorker, backWorker, SelectedComName /*, _debugLogger*/);
                    _commandSenderHostSettable.SetCommandSender(sender);
                }


                _isPortOpened = true;
                OpenPortCommand.RaiseCanExecuteChanged();
                ClosePortCommand.RaiseCanExecuteChanged();
                _logger.Log("Порт " + _selectedComName + " открыт");

                _notifySendingEnabled.SetIsSendingEnabledAndRaiseChange(true);
            }
            catch (Exception ex)
            {
                _logger.Log("Не удалось открыть порт " + _selectedComName + ". " + ex.Message);
            }
        }

        private void GetPortsAvailable()
        {
            var ports = new List<string>();
            ports.AddRange(SerialPort.GetPortNames());
            ports.Add(TestComPortName); // TODO: extract constant);
            ComPortsAvailable = ports;
            if (ComPortsAvailable.Count > 0) SelectedComName = ComPortsAvailable[0];
        }
    }
}