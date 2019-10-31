using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace
{
    /// <summary>
    ///     Хранилище настроек двигателя с возможностью записи настроек в хранилище
    /// </summary>
    public interface IEngineSettingsStorageSettable : IEngineSettingsStorage
    {
        void SetSettings(IEngineSettings settings);
    }
}