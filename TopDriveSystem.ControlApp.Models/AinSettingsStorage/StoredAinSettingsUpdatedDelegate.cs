using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ControlApp.Models.AinSettingsStorage
{
    public delegate void StoredAinSettingsUpdatedDelegate(byte zeroBasedAinNumber, IAinSettings settings);
}