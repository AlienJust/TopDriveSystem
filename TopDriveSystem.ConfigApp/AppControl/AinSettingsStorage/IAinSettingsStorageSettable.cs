using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsStorage
{
    internal interface IAinSettingsStorageSettable : IAinSettingsStorage
    {
        void SetSettings(byte zeroBasedAinNumber, IAinSettings settings);
    }
}