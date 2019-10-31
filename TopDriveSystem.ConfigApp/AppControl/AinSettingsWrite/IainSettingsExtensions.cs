using System;
using AlienJust.Support.Reflection;
using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsWrite {
	public static class IainSettingsExtensions {
		public static void CompareSettingsAfterReReading(this IAinSettings settings, IAinSettings settingsReReaded, int zeroBasedAinNumber) {
			if (zeroBasedAinNumber == 0) {
				if (settingsReReaded.Ain1LinkFault) 
					throw new Exception("��� ��������� ������ (��� ������������� ������) ��������� ���� ��������, ������ ����� � ���1 ������������ (������� ���� ������ �����)");
			}
			else if (zeroBasedAinNumber == 1) {
				if (settingsReReaded.Ain2LinkFault)
					throw new Exception("��� ��������� ������ (��� ������������� ������) ��������� ���� ��������, ������ ����� � ���2 ������������ (������� ���� ������ �����)");
			}
			else if (zeroBasedAinNumber == 2) {
				if (settingsReReaded.Ain3LinkFault)
					throw new Exception("��� ��������� ������ (��� ������������� ������) ��������� ���� ��������, ������ ����� � ���3 ������������ (������� ���� ������ �����)");
			}

			string paramsText = string.Empty;

			if (settings.KpW != settingsReReaded.KpW) paramsText += $"{Environment.NewLine}�������� KpW ��� {settings.KpW:f20}; ���� {settingsReReaded.KpW:f20}";
			if (settings.KiW != settingsReReaded.KiW) paramsText += $"{Environment.NewLine}�������� KiW ��� {settings.KiW:f20}; ���� {settingsReReaded.KiW:f20}";

			if (settings.FiNom != settingsReReaded.FiNom) paramsText += $"{Environment.NewLine}�������� FiNom ��� {settings.FiNom}; ���� {settingsReReaded.FiNom}";
			if (settings.Imax != settingsReReaded.Imax) paramsText += $"{Environment.NewLine}�������� Imax ��� {settings.Imax}; ���� {settingsReReaded.Imax}";
			if (settings.UdcMax != settingsReReaded.UdcMax) paramsText += $"{Environment.NewLine}�������� UdcMax ��� {settings.UdcMax}; ���� {settingsReReaded.UdcMax}";
			if (settings.UdcMin != settingsReReaded.UdcMin) paramsText += $"{Environment.NewLine}�������� UdcMin ��� {settings.UdcMin}; ���� {settingsReReaded.UdcMin}";
			if (settings.Fnom != settingsReReaded.Fnom) paramsText += $"{Environment.NewLine}�������� Fnom ��� {settings.Fnom:f10}; ���� {settingsReReaded.Fnom:f10}";
			if (settings.Fmax != settingsReReaded.Fmax) paramsText += $"{Environment.NewLine}�������� Fmax ��� {settings.Fmax:f10}; ���� {settingsReReaded.Fmax:f10}";
			if (settings.DflLim != settingsReReaded.DflLim) paramsText += $"{Environment.NewLine}�������� DflLim ��� {settings.DflLim:f10}; ���� {settingsReReaded.DflLim:f10}";
			if (settings.FlMinMin != settingsReReaded.FlMinMin) paramsText += $"{Environment.NewLine}�������� FlMinMin ��� {settings.FlMinMin}; ���� {settingsReReaded.FlMinMin}";
			if (settings.IoutMax != settingsReReaded.IoutMax) paramsText += $"{Environment.NewLine}�������� IoutMax ��� {settings.IoutMax}; ���� {settingsReReaded.IoutMax}";
			if (settings.FiMin != settingsReReaded.FiMin) paramsText += $"{Environment.NewLine}�������� FiMin ��� {settings.FiMin}; ���� {settingsReReaded.FiMin}";
			if (settings.DacCh != settingsReReaded.DacCh) paramsText += $"{Environment.NewLine}�������� DacCh ��� {settings.DacCh}; ���� {settingsReReaded.DacCh}";
			if (settings.Imcw != settingsReReaded.Imcw) paramsText += $"{Environment.NewLine}�������� Imcw ��� 0x{settings.Imcw:X4}; ���� 0x{settingsReReaded.Imcw:X4}";
			if (settings.Ia0 != settingsReReaded.Ia0) paramsText += $"{Environment.NewLine}�������� Ia0 ��� {settings.Ia0}; ���� {settingsReReaded.Ia0}";
			if (settings.Ib0 != settingsReReaded.Ib0) paramsText += $"{Environment.NewLine}�������� Ib0 ��� {settings.Ib0}; ���� {settingsReReaded.Ib0}";
			if (settings.Ic0 != settingsReReaded.Ic0) paramsText += $"{Environment.NewLine}�������� Ic0 ��� {settings.Ic0}; ���� {settingsReReaded.Ic0}";
			if (settings.Udc0 != settingsReReaded.Udc0) paramsText += $"{Environment.NewLine}�������� Udc0 ��� {settings.Udc0}; ���� {settingsReReaded.Udc0}";
			if (settings.TauR != settingsReReaded.TauR) paramsText += $"{Environment.NewLine}�������� TauR ��� {settings.TauR:f10}; ���� {settingsReReaded.TauR:f10}";
			if (settings.Lm != settingsReReaded.Lm) paramsText += $"{Environment.NewLine}�������� Lm ��� {settings.Lm:f10}; ���� {settingsReReaded.Lm:f10}";
			if (settings.Lsl != settingsReReaded.Lsl) paramsText += $"{Environment.NewLine}�������� Lsl ��� {settings.Lsl:f10}; ���� {settingsReReaded.Lsl:f10}";
			if (settings.Lrl != settingsReReaded.Lrl) paramsText += $"{Environment.NewLine}�������� Lrl ��� {settings.Lrl:f10}; ���� {settingsReReaded.Lrl:f10}";

			if (settings.KpFi != settingsReReaded.KpFi) paramsText += $"{Environment.NewLine}�������� KpFi ��� {settings.KpFi:f10}; ���� {settingsReReaded.KpFi:f10}";
			if (settings.KiFi != settingsReReaded.KiFi) paramsText += $"{Environment.NewLine}�������� KiFi ��� {settings.KiFi:f10}; ���� {settingsReReaded.KiFi:f10}";

			if (settings.KpId != settingsReReaded.KpId) paramsText += $"{Environment.NewLine}�������� KpId ��� {settings.KpId:f10}; ���� {settingsReReaded.KpId:f10}";
			if (settings.KiId != settingsReReaded.KiId) paramsText += $"{Environment.NewLine}�������� KiId ��� {settings.KiId:f10}; ���� {settingsReReaded.KiId:f10}";
			if (settings.KpIq != settingsReReaded.KpIq) paramsText += $"{Environment.NewLine}�������� KpIq ��� {settings.KpIq:f10}; ���� {settingsReReaded.KpIq:f10}";
			if (settings.KiIq != settingsReReaded.KiIq) paramsText += $"{Environment.NewLine}�������� KiIq ��� {settings.KiIq:f10}; ���� {settingsReReaded.KiIq:f10}";

			if (settings.AccDfDt != settingsReReaded.AccDfDt) paramsText += $"{Environment.NewLine}�������� AccDfDt ��� {settings.AccDfDt}; ���� {settingsReReaded.AccDfDt}";
			if (settings.DecDfDt != settingsReReaded.DecDfDt) paramsText += $"{Environment.NewLine}�������� DecDfDt ��� {settings.DecDfDt}; ���� {settingsReReaded.DecDfDt}";

			if (settings.Unom != settingsReReaded.Unom) paramsText += $"{Environment.NewLine}�������� Unom ��� {settings.Unom:f10}; ���� {settingsReReaded.Unom:f10}";
			if (settings.TauFlLim != settingsReReaded.TauFlLim) paramsText += $"{Environment.NewLine}�������� TauFlLim ��� {settings.TauFlLim:f10}; ���� {settingsReReaded.TauFlLim:f10}";
			if (settings.Rs != settingsReReaded.Rs) paramsText += $"{Environment.NewLine}�������� Rs ��� {settings.Rs:f10}; ���� {settingsReReaded.Rs:f10}";
			if (settings.Fmin != settingsReReaded.Fmin) paramsText += $"{Environment.NewLine}�������� Fmin ��� {settings.Fmin:f10}; ���� {settingsReReaded.Fmin:f10}";

			if (settings.TauM != settingsReReaded.TauM) paramsText += $"{Environment.NewLine}�������� TauM ��� {settings.TauM}; ���� {settingsReReaded.TauM}";
			if (settings.TauF != settingsReReaded.TauF) paramsText += $"{Environment.NewLine}�������� TauF ��� {settings.TauF}; ���� {settingsReReaded.TauF}";
			if (settings.TauFSet != settingsReReaded.TauFSet) paramsText += $"{Environment.NewLine}�������� TauFSet ��� {settings.TauFSet}; ���� {settingsReReaded.TauFSet}";
			if (settings.TauFi != settingsReReaded.TauFi) paramsText += $"{Environment.NewLine}�������� TauFi ��� {settings.TauFi}; ���� {settingsReReaded.TauFi}";
			if (settings.IdSetMin != settingsReReaded.IdSetMin) paramsText += $"{Environment.NewLine}�������� IdSetMin ��� {settings.IdSetMin}; ���� {settingsReReaded.IdSetMin}";
			if (settings.IdSetMax != settingsReReaded.IdSetMax) paramsText += $"{Environment.NewLine}�������� {ReflectedProperty.GetName(()=>settings.IdSetMax)} ��� {settings.IdSetMax}; ���� {settingsReReaded.IdSetMax}";
			if (settings.UchMin != settingsReReaded.UchMin) paramsText += $"{Environment.NewLine}�������� {ReflectedProperty.GetName(() => settings.UchMin)} ��� {settings.UchMin}; ���� {settingsReReaded.UchMin}";
			if (settings.UchMax != settingsReReaded.UchMax) paramsText += $"{Environment.NewLine}�������� {ReflectedProperty.GetName(() => settings.UchMax)} ��� {settings.UchMax}; ���� {settingsReReaded.UchMax}";

			if (settings.Np != settingsReReaded.Np) paramsText += $"{Environment.NewLine}�������� {ReflectedProperty.GetName(() => settings.Np)} ��� {settings.Np}; ���� {settingsReReaded.Np}";
			if (settings.NimpFloorCode != settingsReReaded.NimpFloorCode) paramsText += $"{Environment.NewLine}�������� {ReflectedProperty.GetName(() => settings.NimpFloorCode)} ��� {settings.NimpFloorCode}; ���� {settingsReReaded.NimpFloorCode}";
			if (settings.FanMode != settingsReReaded.FanMode) paramsText += $"{Environment.NewLine}�������� {ReflectedProperty.GetName(() => settings.FanMode)} ��� {settings.FanMode.ToIoBits()}; ���� {settingsReReaded.FanMode.ToIoBits()}";
			if (settings.DirectCurrentMagnetization != settingsReReaded.DirectCurrentMagnetization) paramsText += $"{Environment.NewLine}�������� {ReflectedProperty.GetName(() => settings.DirectCurrentMagnetization)} ��� {settings.DirectCurrentMagnetization}; ���� {settingsReReaded.DirectCurrentMagnetization}";

			if (settings.UmodThr != settingsReReaded.UmodThr) paramsText += $"{Environment.NewLine}�������� {ReflectedProperty.GetName(() => settings.UmodThr)} ��� {settings.UmodThr:f10}; ���� {settingsReReaded.UmodThr:f10}";
			if (settings.EmdecDfdt != settingsReReaded.EmdecDfdt) paramsText += $"{Environment.NewLine}�������� {ReflectedProperty.GetName(() => settings.EmdecDfdt)} ��� {settings.EmdecDfdt}; ���� {settingsReReaded.EmdecDfdt}";
			if (settings.TextMax != settingsReReaded.TextMax) paramsText += $"{Environment.NewLine}�������� {ReflectedProperty.GetName(() => settings.TextMax)} ��� {settings.TextMax}; ���� {settingsReReaded.TextMax}";
			if (settings.ToHl != settingsReReaded.ToHl) paramsText += $"{Environment.NewLine}�������� {ReflectedProperty.GetName(() => settings.ToHl)} ��� {settings.ToHl}; ���� {settingsReReaded.ToHl}";

			if (paramsText != string.Empty) throw new Exception("������ ��� ��������� ��������� �������� � ����������� ������ ��������: " + paramsText);
		}
	}
}