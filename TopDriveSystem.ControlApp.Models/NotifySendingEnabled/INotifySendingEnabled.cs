namespace TopDriveSystem.ControlApp.Models.NotifySendingEnabled
{
    /// <summary>
    ///     Позволяет узнать (в т.ч. и по событию), разрешена отправка команд, или нет.
    /// </summary>
    public interface INotifySendingEnabled
    {
        bool IsSendingEnabled { get; }
        event SendingEnabledChangedDelegate SendingEnabledChanged;
    }
}