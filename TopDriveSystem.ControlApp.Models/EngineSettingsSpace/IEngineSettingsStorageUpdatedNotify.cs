﻿namespace TopDriveSystem.ControlApp.Models.EngineSettingsSpace
{
    /// <summary>
    ///     Сообщает о том, что настройки были обновлены
    /// </summary>
    public interface IEngineSettingsStorageUpdatedNotify
    {
        /// <summary>
        ///     Возникает при обновлении настроек в хранилище
        /// </summary>
        event StoredEngineSettingsUpdatedDelegate EngineSettingsUpdated;
    }
}