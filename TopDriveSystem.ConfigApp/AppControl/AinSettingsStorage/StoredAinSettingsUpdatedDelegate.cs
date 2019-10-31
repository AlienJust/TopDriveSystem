using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsStorage {
	delegate void StoredAinSettingsUpdatedDelegate(byte zeroBasedAinNumber, IAinSettings settings);
}