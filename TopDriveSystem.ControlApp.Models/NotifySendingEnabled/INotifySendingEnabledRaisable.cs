namespace TopDriveSystem.ControlApp.Models.NotifySendingEnabled
{
    public interface INotifySendingEnabledRaisable : INotifySendingEnabled
    {
        void SetIsSendingEnabledAndRaiseChange(bool isSendingEnabled);
    }
}