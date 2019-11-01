using System;
using AlienJust.Support.Mvvm;
using AlienJust.Support.Reflection;
using TopDriveSystem.Commands.BsEthernetLogs;

namespace TopDriveSystem.ConfigApp.BsEthernetLogs
{
    internal class WindowViewModel : ViewModelBase, IDisposable
    {
        private readonly string _logTextName;
        private readonly IReadCycleModel _model;
        private readonly IUserInterfaceRoot _uiRoot;
        private int _errorsCount;

        private IBsEthernetLogLine _lastLogLine;

        private string _logText;
        
        //public Action<string> AppendTextAction { get; set; }

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

        public WindowViewModel(IUserInterfaceRoot uiRoot, ReadCycleModel model)
        {
            _uiRoot = uiRoot;

            _logTextName = ReflectedProperty.GetName(() => LogText);
            LogText = string.Empty;

            _lastLogLine = null;

            _errorsCount = 0;

            _model = model;
            _model.AnotherLogLineWasReaded += ModelOnAnotherLogLineWasReaded; // TODO: unsubscribe on win close, also _destroy model
        }

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


        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
                IsActive = false;
                RaisePropertyChanged(() => IsActive);
                _model.StopBackgroundThreadAndWaitForIt();
                _model.AnotherLogLineWasReaded -= ModelOnAnotherLogLineWasReaded;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposedValue = true;
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~WindowViewModel()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}