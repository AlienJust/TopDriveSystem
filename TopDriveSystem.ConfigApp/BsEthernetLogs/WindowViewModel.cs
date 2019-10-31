using System;
using System.Windows.Input;
using AlienJust.Support.Mvvm;
using AlienJust.Support.Reflection;
using TopDriveSystem.Commands.BsEthernetLogs;

namespace TopDriveSystem.ConfigApp.BsEthernetLogs
{
    internal class WindowViewModel : ViewModelBase
    {
        private readonly RelayCommand _closingWindowCommand;
        private readonly string _logTextName;
        private readonly IReadCycleModel _model;
        private readonly IUserInterfaceRoot _uiRoot;
        private int _errorsCount;

        private IBsEthernetLogLine _lastLogLine;

        private string _logText;

        public WindowViewModel(IUserInterfaceRoot uiRoot, ReadCycleModel model)
        {
            _uiRoot = uiRoot;

            _logTextName = ReflectedProperty.GetName(() => LogText);
            LogText = string.Empty;

            _closingWindowCommand = new RelayCommand(WindowClosing, () => true);
            _lastLogLine = null;

            _errorsCount = 0;

            //_model = new ReadCycleModel(commandSenderHost, targetAddressHost, notifySendingEnabled);
            _model = model;
            _model.AnotherLogLineWasReaded +=
                ModelOnAnotherLogLineWasReaded; // TODO: unsubscribe on win close, also _destroy model
        }

        public Action<string> AppendTextAction { get; set; }


        public bool IsActive
        {
            get => _model.IsReadCycleEnabled;
            set => _model.IsReadCycleEnabled = value;
        }


        public string LogText
        {
            get => _logText;
            set
            {
                if (_logText != value)
                {
                    _logText = value;
                    RaisePropertyChanged(_logTextName);
                }
            }
        }

        public ICommand ClosingWindowCommand => _closingWindowCommand;


        private void ModelOnAnotherLogLineWasReaded(IBsEthernetLogLine logLine)
        {
            _uiRoot.Notifier.Notify(() =>
            {
                if (logLine == null)
                {
                    if (_errorsCount <= 5) _errorsCount++;
                    if (_errorsCount == 5)
                        _uiRoot.Notifier.Notify(() => { LogText = "[ER]" + Environment.NewLine + LogText; });
                }
                else
                {
                    _errorsCount = 0;
                    if (_lastLogLine == null || _lastLogLine.Number != logLine.Number)
                    {
                        _uiRoot.Notifier.Notify(() =>
                        {
                            LogText = "[OK] " + logLine.Number.ToString("d5") + " > " + logLine.Content +
                                      Environment.NewLine + LogText;
                        });
                        _lastLogLine = logLine;
                    }
                }

                //RaisePropertyChanged(_logTextName);
            });
        }


        private void WindowClosing()
        {
            IsActive = false;
            RaisePropertyChanged(() => IsActive);

            _model.StopBackgroundThreadAndWaitForIt();
        }
    }
}