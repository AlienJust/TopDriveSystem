namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsRead
{
    /// <summary>
    ///     Сообщает о том, что настройки были прочитаны
    /// </summary>
    public interface IAinSettingsReadNotify
    {
        event AinSettingsReadStartedDelegate AinSettingsReadStarted;
        event AinSettingsReadCompleteDelegate AinSettingsReadComplete;
    }
}