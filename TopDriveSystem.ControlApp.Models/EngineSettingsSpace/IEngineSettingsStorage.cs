using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace
{
    /// <summary>
    ///     Хранилище настроек двигателя
    /// </summary>
    public interface IEngineSettingsStorage
    {
        /// <summary>
        ///     Сохранённые ранее настройки двигателя
        /// </summary>
        IEngineSettings EngineSettings { get; }
    }
}