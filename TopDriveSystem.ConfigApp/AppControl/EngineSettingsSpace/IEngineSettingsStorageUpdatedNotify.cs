﻿namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace
{
	/// <summary>
	///     Сообщает о том, что настройки были обновлены
	/// </summary>
	internal interface IEngineSettingsStorageUpdatedNotify
    {
	    /// <summary>
	    ///     Возникает при обновлении настроек в хранилище
	    /// </summary>
	    event StoredEngineSettingsUpdatedDelegate EngineSettingsUpdated;
    }
}