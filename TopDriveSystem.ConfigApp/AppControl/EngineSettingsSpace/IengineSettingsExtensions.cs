using System;
using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace {
	public static class IengineSettingsExtensions {
		public static void CompareSettingsAfterReReading(this IEngineSettings settings, IEngineSettings settingsReReaded, int zeroBasedAinNumber) {
			if (settings.Inom != settingsReReaded.Inom) throw new Exception("��� ��������� ������ ���������� ��������� �� ������� � ������������� (�������� Inom)");
			if (settings.Nnom != settingsReReaded.Nnom) throw new Exception("��� ��������� ������ ���������� ��������� �� ������� � ������������� (�������� Nnom)");
			if (settings.Nmax != settingsReReaded.Nmax) throw new Exception("��� ��������� ������ ���������� ��������� �� ������� � ������������� (�������� Nmax)");
			if (settings.Pnom != settingsReReaded.Pnom) throw new Exception("��� ��������� ������ ���������� ��������� �� ������� � ������������� (�������� Pnom)");
			if (settings.CosFi != settingsReReaded.CosFi) throw new Exception("��� ��������� ������ ���������� ��������� �� ������� � ������������� (�������� CosFi)");
			if (settings.Eff != settingsReReaded.Eff) throw new Exception("��� ��������� ������ ���������� ��������� �� ������� � ������������� (�������� Eff)");
			if (settings.Mass != settingsReReaded.Mass) throw new Exception("��� ��������� ������ ���������� ��������� �� ������� � ������������� (�������� Mass)");
			if (settings.MmM != settingsReReaded.MmM) throw new Exception("��� ��������� ������ ���������� ��������� �� ������� � ������������� (�������� MmM)");
			if (settings.Height != settingsReReaded.Height) throw new Exception("��� ��������� ������ ���������� ��������� �� ������� � ������������� (�������� Height)");
			if (settings.I2Tmax != settingsReReaded.I2Tmax) throw new Exception("��� ��������� ������ ���������� ��������� �� ������� � ������������� (�������� I2Tmax)");
			if (settings.Icontinious != settingsReReaded.Icontinious) throw new Exception("��� ��������� ������ ���������� ��������� �� ������� � ������������� (�������� Icontinious)");
			if (settings.ZeroF != settingsReReaded.ZeroF) throw new Exception("��� ��������� ������ ���������� ��������� �� ������� � ������������� (�������� ZeroF)");
		}
	}
}