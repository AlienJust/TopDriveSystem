using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsStorage
{
    internal delegate void StoredAinSettingsUpdatedDelegate(byte zeroBasedAinNumber, IAinSettings settings);
}