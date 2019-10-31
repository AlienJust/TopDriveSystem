using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsStorage
{
	/// <summary>
	///     ��������� �������� ������������ ������ ����� ��� ������ �������� ���.
	///     ���� ��� �������� � ���������, ������ �������� ��������������� �� �����.
	/// </summary>
	internal interface IAinSettingsStorage
    {
        IAinSettings GetSettings(byte zeroBasedAinNumber);
    }
}