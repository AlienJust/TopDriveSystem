using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ControlApp.Models.EngineSettingsSpace
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