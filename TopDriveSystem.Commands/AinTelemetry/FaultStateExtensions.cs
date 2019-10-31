using System;

namespace TopDriveSystem.Commands.AinTelemetry
{
    public static class FaultStateExtensions
    {
        public static ushort ToUshort(this FaultState state)
        {
            return state switch
            {
                FaultState.NoError => 0,
                FaultState.RuleImcwConflict => 1,
                FaultState.RuleAinConflict => 2,
                FaultState.NoAinLink => 3,
                FaultState.NotMagnetized => 4,
                FaultState.SpeedLimit => 5,
                FaultState.StatusError => 6,
                FaultState.UdcLow => 7,

                FaultState.AinLinkError => 8,
                FaultState.EthernetLinkError => 9,
                FaultState.CanLinkError => 10,

                FaultState.ChangedAinMode => 11,
                FaultState.SlaveNotReady => 12,

                FaultState.RelayBlocking => 13,
                FaultState.RelayAlarmMo => 14,

                FaultState.OverheatProtection => 15,
                FaultState.SystemStart => 16,
                FaultState.ChangedControlSource => 17,
                _ => throw new Exception("Cannot convert such state to ushort"),
            };
        }

        public static string ToText(this FaultState state)
        {
            return state switch
            {
                FaultState.NoError => "NO_ERROR",
                FaultState.RuleImcwConflict => "RuleImcwConflict",
                FaultState.RuleAinConflict => "RuleAinConflict",
                FaultState.NoAinLink => "NoAinLink",
                FaultState.NotMagnetized => "NotMagnetized",
                FaultState.SpeedLimit => "SpeedLimit",
                FaultState.StatusError => "StatusError",
                FaultState.UdcLow => "UDC_LOW",

                FaultState.AinLinkError => "AIN_LINK_ERROR",
                FaultState.EthernetLinkError => "ETHERNET_LINK_ERROR",
                FaultState.CanLinkError => "CAN_LINK_ERROR",

                FaultState.ChangedAinMode => "CHANGED_AIN_MODE",
                FaultState.SlaveNotReady => "SLAVE_NOT_READY",
                FaultState.RelayBlocking => "RELAY_BLOCKING",
                FaultState.RelayAlarmMo => "RELAY_ALARM_MO",

                FaultState.OverheatProtection => "OVERHEAT_PROTECTION",
                FaultState.SystemStart => "SYSTEM_START",
                FaultState.ChangedControlSource => "CHANGED_CONTROL_SOURCE",
                _ => throw new Exception("Cannot convert such state to string"),
            };
        }

        public static FaultState GetStateFromUshort(ushort value)
        {
            return value switch
            {
                0 => FaultState.NoError,
                1 => FaultState.RuleImcwConflict,
                2 => FaultState.RuleAinConflict,
                3 => FaultState.NoAinLink,
                4 => FaultState.NotMagnetized,
                5 => FaultState.SpeedLimit,
                6 => FaultState.StatusError,
                7 => FaultState.UdcLow,

                8 => FaultState.AinLinkError,
                9 => FaultState.EthernetLinkError,
                10 => FaultState.CanLinkError,

                11 => FaultState.ChangedAinMode,
                12 => FaultState.SlaveNotReady,

                13 => FaultState.RelayBlocking,
                14 => FaultState.RelayAlarmMo,
                15 => FaultState.OverheatProtection,
                16 => FaultState.SystemStart,
                17 => FaultState.ChangedControlSource,

                _ => throw new Exception("Cannot get ushort " + value + " as " + typeof(FaultState).Name),
            };
        }
    }
}