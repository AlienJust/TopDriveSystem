using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace
{
	/// <summary>
	/// ��������� �������� ���������
	/// </summary>
	interface IEngineSettingsStorage
	{
		/// <summary>
		/// ����������� ����� ��������� ���������
		/// </summary>
		IEngineSettings EngineSettings { get; }
	}
}