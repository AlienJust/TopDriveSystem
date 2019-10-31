using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsStorage {
	interface IAinSettingsStorageSettable : IAinSettingsStorage {
		void SetSettings(byte zeroBasedAinNumber, IAinSettings settings);
	}
}