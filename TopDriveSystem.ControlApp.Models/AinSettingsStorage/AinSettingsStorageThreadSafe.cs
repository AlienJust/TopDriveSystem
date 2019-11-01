using System;
using System.Collections.Generic;
using TopDriveSystem.Commands.AinSettings;
using TopDriveSystem.ControlApp.Models.AinsCounter;
using TopDriveSystem.ControlApp.Models.NotifySendingEnabled;

namespace TopDriveSystem.ControlApp.Models.AinSettingsStorage
{
    public sealed class AinSettingsStorageThreadSafe : IAinSettingsStorageSettable, IAinSettingsStorageUpdatedNotify, IDisposable
    {
        private readonly IAinsCounter _ainsCounter;
        private readonly INotifySendingEnabled _sendingEnabledMonitor;
        private readonly object _ainSettingsSync;
        private readonly List<IAinSettings> _ainsSettings;
        
        public AinSettingsStorageThreadSafe(IAinsCounter ainsCounter, INotifySendingEnabled sendingEnabledMonitor)
        {
            _ainsCounter = ainsCounter;
            _sendingEnabledMonitor = sendingEnabledMonitor;
            _ainSettingsSync = new object();
            _ainsSettings = new List<IAinSettings> {null, null, null};
            
            _ainsCounter.AinsCountInSystemHasBeenChanged += AinsCounterOnAinsCountInSystemHasBeenChanged;
            _sendingEnabledMonitor.SendingEnabledChanged += SendingEnabledMonitorOnSendingEnabledChanged;
        }

        private void SendingEnabledMonitorOnSendingEnabledChanged(bool issendingenabled)
        {
            if (!issendingenabled)
                for (byte i = 0; i < _ainsCounter.SelectedAinsCount; ++i)
                    SetSettings(i, null);
        }

        private void AinsCounterOnAinsCountInSystemHasBeenChanged(int ainscount)
        {
            for (byte i = (byte)ainscount; i < 3; ++i) {
                SetSettings(i, null);
            }
        }

        public IAinSettings GetSettings(byte zeroBasedAinNumber)
        {
            lock (_ainSettingsSync)
            {
                return _ainsSettings[zeroBasedAinNumber];
            }
        }

        public void SetSettings(byte zeroBasedAinNumber, IAinSettings settings)
        {
            lock (_ainSettingsSync)
            {
                _ainsSettings[zeroBasedAinNumber] = settings;
            }

            RaiseAinSettingsUpdated(zeroBasedAinNumber, settings);
        }

        public event StoredAinSettingsUpdatedDelegate AinSettingsUpdated;

        private void RaiseAinSettingsUpdated(byte zeroBasedAinNumber, IAinSettings settings)
        {
            var eve = AinSettingsUpdated;
            eve?.Invoke(zeroBasedAinNumber, settings);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _ainsCounter.AinsCountInSystemHasBeenChanged -= AinsCounterOnAinsCountInSystemHasBeenChanged;
                    _sendingEnabledMonitor.SendingEnabledChanged -= SendingEnabledMonitorOnSendingEnabledChanged;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AinSettingsStorageThreadSafe()
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