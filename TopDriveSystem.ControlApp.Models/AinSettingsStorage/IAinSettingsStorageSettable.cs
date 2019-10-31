using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ControlApp.Models.AinSettingsStorage
{
    public interface IAinSettingsStorageSettable : IAinSettingsStorage
    {
        void SetSettings(byte zeroBasedAinNumber, IAinSettings settings);
    }
}