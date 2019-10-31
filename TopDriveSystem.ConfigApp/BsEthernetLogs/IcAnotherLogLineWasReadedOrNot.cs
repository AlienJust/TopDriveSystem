using TopDriveSystem.Commands.BsEthernetLogs;

namespace TopDriveSystem.ConfigApp.BsEthernetLogs
{
	/// <summary>
	///     ��������� ������ ����� ���� ������� ��� ���
	/// </summary>
	/// <param name="logLine">������ ��������� ������ ����� ��� null</param>
	internal delegate void IcAnotherLogLineWasReadedOrNot(IBsEthernetLogLine logLine);
}