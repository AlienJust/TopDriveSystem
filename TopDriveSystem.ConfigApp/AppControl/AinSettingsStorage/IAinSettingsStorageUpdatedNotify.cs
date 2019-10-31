namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsStorage
{
	/// <summary>
	///     Сообщает о том, что настройки были обновлены
	/// </summary>
	internal interface IAinSettingsStorageUpdatedNotify
    {
        event StoredAinSettingsUpdatedDelegate AinSettingsUpdated;
    }
}