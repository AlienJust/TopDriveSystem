namespace TopDriveSystem.ControlApp.Models.AinSettingsStorage
{
    /// <summary>
    ///     Сообщает о том, что настройки были обновлены
    /// </summary>
    public interface IAinSettingsStorageUpdatedNotify
    {
        event StoredAinSettingsUpdatedDelegate AinSettingsUpdated;
    }
}