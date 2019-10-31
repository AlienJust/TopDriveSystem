namespace TopDriveSystem.ConfigApp.AppControl.NotifySendingEnabled
{
    public interface INotifySendingEnabledRaisable : INotifySendingEnabled
    {
        void SetIsSendingEnabledAndRaiseChange(bool isSendingEnabled);
    }
}