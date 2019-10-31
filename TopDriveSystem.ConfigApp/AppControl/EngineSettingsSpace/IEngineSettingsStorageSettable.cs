using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace
{
	/// <summary>
	/// ��������� �������� ��������� � ������������ ������ �������� � ���������
	/// </summary>
	interface IEngineSettingsStorageSettable : IEngineSettingsStorage
	{
		void SetSettings(IEngineSettings settings);
	}
}