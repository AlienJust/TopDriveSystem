using System;

namespace TopDriveSystem.Commands.AinTelemetry
{
    public static class FaultStateExtensions
    {
        public static ushort ToUshort(this FaultState state)
        {
            return state switch
            {
                FaultState.NoError => (ushort)0,
                FaultState.RuleImcwConflict => (ushort)1,
                FaultState.RuleAinConflict => (ushort)2,
                FaultState.NoAinLink => (ushort)3,
                FaultState.NotMagnetized => (ushort)4,
                FaultState.SpeedLimit => (ushort)5,
                FaultState.StatusError => (ushort)6,
                FaultState.UdcLow => (ushort)7,

                FaultState.AinLinkError => (ushort)8,
                FaultState.EthernetLinkError => (ushort)9,
                FaultState.CanLinkError => (ushort)10,

                FaultState.ChangedAinMode => (ushort)11,
                FaultState.SlaveNotReady => (ushort)12,

                FaultState.RelayBlocking => (ushort)13,
                FaultState.RelayAlarmMo => (ushort)14,

                FaultState.OverheatProtection => (ushort)15,
                FaultState.SystemStart => (ushort)16,
                FaultState.ChangedControlSource => (ushort)17,
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