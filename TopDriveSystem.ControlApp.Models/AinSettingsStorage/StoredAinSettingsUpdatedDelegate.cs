using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsStorage
{
    public delegate void StoredAinSettingsUpdatedDelegate(byte zeroBasedAinNumber, IAinSettings settings);
}