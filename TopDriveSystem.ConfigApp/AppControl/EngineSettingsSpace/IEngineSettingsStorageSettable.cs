using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace
{
	/// <summary>
	///     ��������� �������� ��������� � ������������ ������ �������� � ���������
	/// </summary>
	internal interface IEngineSettingsStorageSettable : IEngineSettingsStorage
    {
        void SetSettings(IEngineSettings settings);
    }
}