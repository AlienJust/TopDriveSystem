namespace TopDriveSystem.ConfigApp.AppControl.NotifySendingEnabled
{
	/// <summary>
	///     Позволяет узнать (в т.ч. и по событию), разрешена отправка команд, или нет.
	/// </summary>
	internal interface INotifySendingEnabled
    {
        bool IsSendingEnabled { get; }
        event SendingEnabledChangedDelegate SendingEnabledChanged;
    }
}