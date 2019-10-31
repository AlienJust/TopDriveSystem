using System.Collections.ObjectModel;
using System.Windows.Input;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Mvvm;
using AlienJust.Support.Text.Contracts;

namespace TopDriveSystem.ConfigApp.Logs
{
    internal class ProgramLogViewModel : ViewModelBase, ILogger
    {
        private readonly IMultiLoggerWithStackTrace<int> _debugLogger;
        private readonly ITextFormatter _formatter;
        private readonly IUserInterfaceRoot _userInterfaceRoot;

        private bool _scrollAutomaticly;
        private ILogLine _selectedLine;

        public ProgramLogViewModel(IUserInterfaceRoot userInterfaceRoot, IMultiLoggerWithStackTrace<int> debugLogger,
            ITextFormatter formatter)
        {
            _userInterfaceRoot = userInterfaceRoot;
            _debugLogger = debugLogger;
            _formatter = formatter;
            LogLines = new ObservableCollection<ILogLine>();

            ClearLogCmd = new RelayCommand(ClearLog);
            ScrollAutomaticly = true;
        }

        public ObservableCollection<ILogLine> LogLines { get; }

        public ICommand ClearLogCmd { get; }

        public bool ScrollAutomaticly
        {
            get => _scrollAutomaticly;
            set
            {
                if (_scrollAutomaticly != value)
                {
                    _scrollAutomaticly = value;
                    RaisePropertyChanged(() => ScrollAutomaticly);
                }
            }
        }

        public ILogLine SelectedLine
        {
            get => _selectedLine;
            set
            {
                if (_selectedLine != value)
                {
                    _selectedLine = value;
                    RaisePropertyChanged(() => SelectedLine);
                }
            }
        }

        public void Log(object obj)
        {
            Log(obj.ToString());
        }

        private void ClearLog()
        {
            LogLines.Clear();
        }


        public void Log(string text)
        {
            _userInterfaceRoot.Notifier.Notify(() =>
            {
                var logLine = new LogLineSimple(_formatter.Format(text));
                LogLines.Add(logLine);
                if (ScrollAutomaticly) SelectedLine = logLine;
            });
            _debugLogger.GetLogger(5).Log(text, null);
        }
    }
}