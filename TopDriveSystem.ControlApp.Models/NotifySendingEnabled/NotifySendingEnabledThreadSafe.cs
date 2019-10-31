namespace TopDriveSystem.ConfigApp.AppControl.NotifySendingEnabled
{
    public sealed class NotifySendingEnabledThreadSafe : INotifySendingEnabledRaisable
    {
        private readonly object _isSendingEnabledSync;
        private bool _isSendingEnabled;

        public NotifySendingEnabledThreadSafe(bool isSendingEnabled)
        {
            _isSendingEnabledSync = new object();
            _isSendingEnabled = isSendingEnabled;
        }

        public event SendingEnabledChangedDelegate SendingEnabledChanged;

        public bool IsSendingEnabled
        {
            get
            {
                lock (_isSendingEnabledSync)
                {
                    return _isSendingEnabled;
                }
            }
        }

        public void SetIsSendingEnabledAndRaiseChange(bool isSendingEnabled)
        {
            lock (_isSendingEnabledSync)
            {
                _isSendingEnabled = isSendingEnabled;
                var eve = SendingEnabledChanged; // TODO: thing if I need lock
                eve?.Invoke(isSendingEnabled);
            }
        }
    }
}