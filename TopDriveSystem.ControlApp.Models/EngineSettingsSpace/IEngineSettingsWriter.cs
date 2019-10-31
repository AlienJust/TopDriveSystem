using System;

namespace TopDriveSystem.ControlApp.Models.EngineSettingsSpace
{
    /// <summary>
    ///     Интерфейс для записи настроек двигателя
    /// </summary>
    public interface IEngineSettingsWriter
    {
        /// <summary>
        ///     Записывает настройки асинхронно
        /// </summary>
        void WriteSettingsAsync(IEngineSettingsPart settings, Action<Exception> callback);
    }
}