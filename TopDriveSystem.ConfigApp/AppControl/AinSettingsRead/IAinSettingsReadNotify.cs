namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsRead
{
	/// <summary>
	///     Сообщает о том, что настройки были прочитаны
	/// </summary>
	internal interface IAinSettingsReadNotify
    {
        event AinSettingsReadStartedDelegate AinSettingsReadStarted;
        event AinSettingsReadCompleteDelegate AinSettingsReadComplete;
    }
}