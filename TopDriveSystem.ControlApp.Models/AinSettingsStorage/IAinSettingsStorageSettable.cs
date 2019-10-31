using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsStorage
{
    public interface IAinSettingsStorageSettable : IAinSettingsStorage
    {
        void SetSettings(byte zeroBasedAinNumber, IAinSettings settings);
    }
}