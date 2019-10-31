using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsStorage {
	/// <summary>
	/// ��������� �������� ������������ ������ ����� ��� ������ �������� ���. 
	/// ���� ��� �������� � ���������, ������ �������� ��������������� �� �����.
	/// </summary>
	interface IAinSettingsStorage {
		IAinSettings GetSettings(byte zeroBasedAinNumber);
	}
}