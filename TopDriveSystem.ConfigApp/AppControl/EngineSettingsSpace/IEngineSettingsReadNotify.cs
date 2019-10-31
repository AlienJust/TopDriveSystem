namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace
{
    /// <summary>
    /// Сообщает о том, что настройки были прочитаны
    /// </summary>
    interface IEngineSettingsReadNotify
    {
        event EngineSettingsReadStartedDelegate EngineSettingsReadStarted;
        event EngineSettingsReadCompleteDelegate EngineSettingsReadComplete;
    }
}