using System;

namespace TopDriveSystem.Commands.AinSettings
{
    public static class AinTelemetryFanWorkmodeExtensions
    {
        public static AinTelemetryFanWorkmode FromIoBits(int bits)
        {
            return bits switch
            {
                0 => AinTelemetryFanWorkmode.AllwaysOff,
                1 => AinTelemetryFanWorkmode.SwitchOnSyncToPwmSwtichOffTwoMinutesLaterAfterPwmOff,
                2 => AinTelemetryFanWorkmode.SwitchOnSyncToPwmSwtichOffAfterPwmOffAndTempGoesDownBelow45C,
                3 => AinTelemetryFanWorkmode.AllwaysOn,
                _ => throw new Exception("Cannot convert " + typeof(int).FullName + " value " + bits + " to " +
                                        typeof(AinTelemetryFanWorkmode).FullName),
            };
        }

        public static int ToIoBits(this AinTelemetryFanWorkmode fanMode)
        {
            return fanMode switch
            {
                AinTelemetryFanWorkmode.AllwaysOff => 0,
                AinTelemetryFanWorkmode.SwitchOnSyncToPwmSwtichOffTwoMinutesLaterAfterPwmOff => 1,
                AinTelemetryFanWorkmode.SwitchOnSyncToPwmSwtichOffAfterPwmOffAndTempGoesDownBelow45C => 2,
                AinTelemetryFanWorkmode.AllwaysOn => 3,
                _ => throw new Exception("Cannot convert " + typeof(AinTelemetryFanWorkmode).FullName + " value to " +
                                        typeof(int).FullName),
            };
        }

        public static string ToHumanString(this AinTelemetryFanWorkmode fanMode)
        {
            return fanMode switch
            {
                AinTelemetryFanWorkmode.AllwaysOff => "Всегда выключен",
                AinTelemetryFanWorkmode.SwitchOnSyncToPwmSwtichOffTwoMinutesLaterAfterPwmOff => "Включение вместе с ШИМ, выключение через 2 минуты после снятия ШИМ",
                AinTelemetryFanWorkmode.SwitchOnSyncToPwmSwtichOffAfterPwmOffAndTempGoesDownBelow45C => "Включение вместе с ШИМ, выключение при снижении температуры ниже 45 градусов после снятия ШИМ",
                AinTelemetryFanWorkmode.AllwaysOn => "Всегда включен",
                _ => throw new Exception("Cannot convert " + typeof(AinTelemetryFanWorkmode).FullName + " value to " +
                                        typeof(string).FullName),
            };
        }
    }
}