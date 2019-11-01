using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Text;
using AlienJust.Support.Text.Contracts;
using AlienJust.Support.Wpf;
using TopDriveSystem.ConfigApp.AinCommand;
using TopDriveSystem.ConfigApp.AinTelemetry;
using TopDriveSystem.ConfigApp.AppControl;
using TopDriveSystem.ConfigApp.BsEthernetLogs;
using TopDriveSystem.ControlApp.Models.AinsCounter;
using TopDriveSystem.ControlApp.Models.AinSettingsRead;
using TopDriveSystem.ControlApp.Models.AinSettingsStorage;
using TopDriveSystem.ControlApp.Models.AinSettingsWrite;
using TopDriveSystem.ControlApp.Models.CommandSenderHost;
using TopDriveSystem.ControlApp.Models.Cycle;
using TopDriveSystem.ControlApp.Models.EngineSettingsSpace;
using TopDriveSystem.ControlApp.Models.LoggerHost;
using TopDriveSystem.ControlApp.Models.NotifySendingEnabled;
using TopDriveSystem.ControlApp.Models.ParamLogger;
using TopDriveSystem.ControlApp.Models.TargetAddressHost;

namespace TopDriveSystem.ConfigApp
{
    public partial class App : Application
    {
        private IAinsCounter _ainsCounter;
        private IAinsCounterRaisable _ainsCounterRaisable;

        private IAinSettingsReader _ainSettingsReader;
        private IAinSettingsReadNotify _ainSettingsReadNotify;
        private IAinSettingsReadNotifyRaisable _ainSettingsReadNotifyRaisable;

        private IAinSettingsStorage _ainSettingsStorage;
        private IAinSettingsStorageSettable _ainSettingsStorageSettable;
        private IAinSettingsStorageUpdatedNotify _ainSettingsStorageUpdatedNotify;
        private IAinSettingsWriter _ainSettingsWriter;
        
        private AutoSettingsReader _autoSettingsReader;
        
        private AutoTimeSetter _autoTimeSetter;

        private ReadCycleModel _bsEthernetLogsReadCycleModel;
        private ICommandSenderHost _cmdSenderHost;
        private ICommandSenderHostSettable _cmdSenderHostSettable;

        private ILogger _commonLogger;

        private IParameterLogger _commonParamLogger;

        private ICycleThreadHolder _cycleThreadHolder;

        private RelayMultiLoggerWithStackTraceSimple _debugLogger;

        private IEngineSettingsReader _engineSettingsReader;
        private IEngineSettingsReadNotify _engineSettingsReadNotify;
        private IEngineSettingsReadNotifyRaisable _engineSettingsReadNotifyRaisable;
        private IEngineSettingsStorage _engineSettingsStorage;
        private IEngineSettingsStorageSettable _engineSettingsStorageSettable;
        private IEngineSettingsStorageUpdatedNotify _engineSettingsStorageUpdatedNotify;
        private IEngineSettingsWriter _engineSettingsWriter;

        private ILoggerRegistrationPoint _loggerRegPoint;

        private INotifySendingEnabled _notifySendingEnabled;

        private INotifySendingEnabledRaisable _notifySendingEnabledRaisable;
        private IParamLoggerRegistrationPoint _paramLoggerRegPoint;
        private ITargetAddressHost _targetAddressHost;

        private ITargetAddressHostSettable _targetAddressHostSettable;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var colorsForGraphics = new List<Color>
            {
                Colors.LawnGreen,
                Colors.Red,
                Colors.Cyan,
                Colors.Yellow,
                Colors.Coral,
                Colors.LightGreen,
                Colors.HotPink,
                Colors.DeepSkyBlue,
                Colors.Gold,
                Colors.Orange,
                Colors.Violet,
                Colors.White,
                Colors.Fuchsia,
                Colors.LightSkyBlue,
                Colors.LightGray,
                Colors.Khaki,
                Colors.SpringGreen,
                Colors.Tomato,
                Colors.LightCyan,
                Colors.Goldenrod,
                Colors.SlateBlue,
                Colors.Cornsilk,
                Colors.MediumPurple,
                Colors.RoyalBlue,
                Colors.MediumVioletRed,
                Colors.MediumTurquoise
            };

            _debugLogger = new RelayMultiLoggerWithStackTraceSimple(
                new RelayLoggerWithStackTrace(
                    new RelayActionLogger(s => { }),
                    new StackTraceFormatterWithNullSuport(" > ", "[NO STACK INFO]")),
                new RelayLoggerWithStackTrace(
                    new RelayLogger(
                        new ColoredConsoleLogger(ConsoleColor.Red, ConsoleColor.Black),
                        new ChainedFormatter(new List<ITextFormatter>
                        {
                            new ThreadFormatter(" > ", true, false, false),
                            new DateTimeFormatter(" > ")
                        })),
                    new StackTraceFormatterWithNullSuport(" > ", "[NO STACK INFO]")),
                new RelayLoggerWithStackTrace(
                    new RelayLogger(
                        new ColoredConsoleLogger(ConsoleColor.Yellow, ConsoleColor.Black),
                        new ChainedFormatter(new List<ITextFormatter>
                        {
                            new ThreadFormatter(" > ", true, false, false),
                            new DateTimeFormatter(" > ")
                        })),
                    new StackTraceFormatterWithNullSuport(" > ", "[NO STACK INFO]")),
                new RelayLoggerWithStackTrace(
                    new RelayLogger(
                        new ColoredConsoleLogger(ConsoleColor.DarkCyan, ConsoleColor.Black),
                        new ChainedFormatter(new List<ITextFormatter>
                        {
                            new ThreadFormatter(" > ", true, false, false),
                            new DateTimeFormatter(" > ")
                        })),
                    new StackTraceFormatterNothing()),
                new RelayLoggerWithStackTrace(
                    new RelayLogger(
                        new ColoredConsoleLogger(ConsoleColor.Cyan, ConsoleColor.Black),
                        new ChainedFormatter(new List<ITextFormatter>
                        {
                            new ThreadFormatter(" > ", true, false, false),
                            new DateTimeFormatter(" > ")
                        })),
                    new StackTraceFormatterNothing()),
                new RelayLoggerWithStackTrace(
                    new RelayLogger(
                        new ColoredConsoleLogger(ConsoleColor.Green, ConsoleColor.Black),
                        new ChainedFormatter(new List<ITextFormatter>
                            {new ThreadFormatter(" > ", false, true, false), new DateTimeFormatter(" > ")})),
                    new StackTraceFormatterWithNullSuport(" > ", string.Empty)),
                new RelayLoggerWithStackTrace(
                    new RelayLogger(
                        new ColoredConsoleLogger(ConsoleColor.White, ConsoleColor.Black),
                        new ChainedFormatter(new List<ITextFormatter>
                            {new ThreadFormatter(" > ", true, false, false), new DateTimeFormatter(" > ")})),
                    new StackTraceFormatterNothing()));

            var loggerAndRegPoint = new LoggerRegistrationPointThreadSafe();
            _commonLogger = loggerAndRegPoint;
            _loggerRegPoint = loggerAndRegPoint;

            var paramLoggerAndRegPoint = new ParamLoggerRegistrationPointThreadSafe();
            _commonParamLogger = paramLoggerAndRegPoint;
            _paramLoggerRegPoint = paramLoggerAndRegPoint;


            var cmdSenderHost = new CommandSenderHostThreadSafe();
            _cmdSenderHostSettable = cmdSenderHost;
            _cmdSenderHost = cmdSenderHost;

            var targetAddressHost = new TargetAddressHostThreadSafe(1);
            _targetAddressHostSettable = targetAddressHost;
            _targetAddressHost = targetAddressHost;

            var notifySendingEnabled = new NotifySendingEnabledThreadSafe(false);
            _notifySendingEnabledRaisable = notifySendingEnabled;
            _notifySendingEnabled = notifySendingEnabled;

            var ainsCounter = new AinsCounterThreadSafe(1);
            _ainsCounterRaisable = ainsCounter;
            _ainsCounter = ainsCounter;

            _cycleThreadHolder = new CycleThreadHolderThreadSafe();

            var ainSettingsStorage = new AinSettingsStorageThreadSafe(_ainsCounter, _notifySendingEnabled);
            _ainSettingsStorage = ainSettingsStorage;
            _ainSettingsStorageSettable = ainSettingsStorage;
            _ainSettingsStorageUpdatedNotify = ainSettingsStorage;

            var ainSettingsReader = new AinSettingsReader(_cmdSenderHost, _targetAddressHost, _commonLogger,
                _ainSettingsStorageSettable, _debugLogger);
            _ainSettingsReader = ainSettingsReader;
            _ainSettingsReadNotify = ainSettingsReader;
            _ainSettingsReadNotifyRaisable = ainSettingsReader;

            _ainSettingsWriter = new AinSettingsWriter(_cmdSenderHost, _targetAddressHost, _ainsCounterRaisable,
                _ainSettingsReader);
            _autoTimeSetter =
                new AutoTimeSetter(_cmdSenderHost, _notifySendingEnabled, _targetAddressHost, _commonLogger);


            var engineSettingsStorage = new EngineSettingsStorageThreadSafe();
            _engineSettingsStorage = engineSettingsStorage;
            _engineSettingsStorageSettable = engineSettingsStorage;
            _engineSettingsStorageUpdatedNotify = engineSettingsStorage;

            var engineSettingsReader = new EngineSettingsReader(_cmdSenderHost, _targetAddressHost, _commonLogger,
                _engineSettingsStorageSettable, _debugLogger);
            _engineSettingsReader = engineSettingsReader;
            _engineSettingsReadNotify = engineSettingsReader;
            _engineSettingsReadNotifyRaisable = engineSettingsReader;

            _engineSettingsWriter = new EngineSettingsWriter(_cmdSenderHost, _targetAddressHost, _engineSettingsReader);


            _autoSettingsReader = new AutoSettingsReader(
                _notifySendingEnabled, 
                _ainsCounterRaisable,
                _ainSettingsReader, 
                _ainSettingsStorageSettable, 
                _commonLogger,
                _engineSettingsReader, 
                _engineSettingsStorageSettable);

            
            
            
            
            _bsEthernetLogsReadCycleModel = new ReadCycleModel(_cmdSenderHost, targetAddressHost, notifySendingEnabled);

            var uiRoot = new SimpleUiRoot(new WpfUiNotifierAsync(Dispatcher.CurrentDispatcher));

            var ainCommandAndCommonTelemetryVm = new AinCommandAndCommonTelemetryViewModel(
                new AinCommandAndMinimalCommonTelemetryViewModel(
                    _cmdSenderHost, 
                    _targetAddressHost, 
                    uiRoot,
                    _commonLogger, 
                    _notifySendingEnabled, 
                    0, 
                    _ainSettingsStorage, 
                    _ainSettingsStorageUpdatedNotify),
                new TelemetryCommonViewModel(),
                _cmdSenderHost, 
                _targetAddressHost, 
                uiRoot, 
                _notifySendingEnabled);
            

            // TODO: Register something but not VM?
            _cycleThreadHolder.RegisterAsCyclePart(ainCommandAndCommonTelemetryVm);


            new CommandWindow { DataContext = new CommandWindowViewModel(ainCommandAndCommonTelemetryVm) }.Show();

            new BsEthernetLogsWindow { DataContext = new WindowViewModel(uiRoot, _bsEthernetLogsReadCycleModel) }.Show();



            var mainViewModel = new MainViewModel(
                    new SimpleUiRoot(new WpfUiNotifierAsync(Dispatcher.CurrentDispatcher)),
                    new WpfWindowSystem(),
                    colorsForGraphics,
                    _cmdSenderHostSettable,
                    _targetAddressHost,
                    _debugLogger,
                    _loggerRegPoint,
                    _notifySendingEnabledRaisable,
                    _commonParamLogger,
                    _ainsCounterRaisable,
                    _cycleThreadHolder,
                    _ainSettingsReader,
                    _ainSettingsReadNotify,
                    _ainSettingsReadNotifyRaisable,
                    _ainSettingsWriter, _ainSettingsStorage, _ainSettingsStorageSettable,
                    _ainSettingsStorageUpdatedNotify, _bsEthernetLogsReadCycleModel,
                    _engineSettingsReader,
                    _engineSettingsWriter,
                    _engineSettingsReadNotify,
                    _engineSettingsReadNotifyRaisable,
                    _engineSettingsStorage,
                    _engineSettingsStorageSettable,
                    _engineSettingsStorageUpdatedNotify);

            new MainWindow{ DataContext = mainViewModel }.Show();
        }
    }
}