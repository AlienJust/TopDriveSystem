using System;

namespace TopDriveSystem.Commands.AinTelemetry
{
    public static class EngineStateExtensions
    {
        public static ushort ToUshort(this EngineState state)
        {
            return state switch
            {
                EngineState.PowerOn => (ushort)0,
                EngineState.WaitStart => (ushort)1,
                EngineState.WaitLinkAin1 => (ushort)2,
                EngineState.TestImcw => (ushort)3,
                EngineState.PreResetErrors => (ushort)4,
                EngineState.ResetErrors => (ushort)5,
                EngineState.NotReady => (ushort)6,
                EngineState.ReadyToSwitchOn => (ushort)7,
                EngineState.ReadyRun => (ushort)8,
                EngineState.EnableOperation => (ushort)9,
                EngineState.EnableOutput => (ushort)10,
                EngineState.AcceleratorEnable => (ushort)11,
                EngineState.OperatingState => (ushort)12,

                EngineState.ReadySlave => (ushort)13,
                EngineState.DriveSlave => (ushort)14,

                EngineState.Off2 => (ushort)15,
                EngineState.Off3 => (ushort)16,
                EngineState.Off1 => (ushort)17,

                EngineState.Inching1 => (ushort)18,
                EngineState.Inching2 => (ushort)19,
                EngineState.PostInching => (ushort)20,

                EngineState.FaultState => (ushort)21,
                EngineState.FaultStateWaitReset => (ushort)22,
                EngineState.SwitchOnInhibit => (ushort)23,
                EngineState.InhibitOperationActive => (ushort)24,
                EngineState.ChopperNotReady => (ushort)25,
                EngineState.ChopperRun => (ushort)26,
                EngineState.ReadKIs => (ushort)27,
                EngineState.ReadMOs => (ushort)28,
                EngineState.AciIdentifyStart => (ushort)29,
                EngineState.AciIdentifyExecution => (ushort)30,
                EngineState.AciIdentifyEnd => (ushort)31,
                EngineState.TramStop => (ushort)32,
                EngineState.TramMagnetizing => (ushort)33,
                EngineState.TramAccelerationForward => (ushort)34,
                EngineState.TramMovingForward => (ushort)35,
                EngineState.TramDecelerationForward => (ushort)36,
                EngineState.TramAccelerationBackward => (ushort)37,
                EngineState.TramMovingBackward => (ushort)38,
                EngineState.TramDecelerationBackward => (ushort)39,
                _ => throw new Exception("Cannot convert such state to ushort"),
            };
        }

        public static string ToText(this EngineState state)
        {
            return state switch
            {
                EngineState.PowerOn => "POWER_ON",
                EngineState.WaitStart => "WAIT_START",
                EngineState.WaitLinkAin1 => "WAIT_LINK_AIN1",
                EngineState.TestImcw => "TestImcw",
                EngineState.PreResetErrors => "PRE_RESET_ERRORS",
                EngineState.ResetErrors => "RESET_ERRORS",
                EngineState.NotReady => "NOT_READY",
                EngineState.ReadyToSwitchOn => "ReadyToSwitchOn",
                EngineState.ReadyRun => "READY_RUN",

                EngineState.EnableOperation => "ENABLE_OPERATION",
                EngineState.EnableOutput => "ENABLE_OUTPUT",
                EngineState.AcceleratorEnable => "ACCELERATOR_ENABLE",
                EngineState.OperatingState => "OPERATING_STATE",

                EngineState.ReadySlave => "READY_SLAVE",
                EngineState.DriveSlave => "DRIVE_SLAVE",

                EngineState.Off2 => "OFF_2",
                EngineState.Off3 => "OFF_3",
                EngineState.Off1 => "OFF_1",
                EngineState.Inching1 => "INCHING1",
                EngineState.Inching2 => "INCHING2",
                EngineState.PostInching => "POST_INCHING",
                EngineState.FaultState => "FAULT_STATE",
                EngineState.FaultStateWaitReset => "FAULT_STATE_WAIT_RESET",
                EngineState.SwitchOnInhibit => "SWITCH_ON_INHIBIT",
                EngineState.InhibitOperationActive => "INHIBIT_OPERATION_ACTIVE",
                EngineState.ChopperNotReady => "CHOPPER_NOT_READY",
                EngineState.ChopperRun => "CHOPPER_RUN",
                EngineState.ReadKIs => "READ_KIs",
                EngineState.ReadMOs => "READ_MOs",
                EngineState.AciIdentifyStart => "ACI_IDENTIFY_START",
                EngineState.AciIdentifyExecution => "ACI_IDENTIFY_EXECUTION",

                EngineState.AciIdentifyEnd => "ACI_IDENTIFY_END",
                EngineState.TramStop => "TRAM_STOP",
                EngineState.TramMagnetizing => "TRAM_MAGNETIZING",
                EngineState.TramAccelerationForward => "TRAM_ACCELERATION_FORWARD",
                EngineState.TramMovingForward => "TRAM_MOVING_FORWARD",
                EngineState.TramDecelerationForward => "TRAM_DECELERATION_FORWARD",
                EngineState.TramAccelerationBackward => "TRAM_ACCELERATION_BACKWARD",
                EngineState.TramMovingBackward => "TRAM_MOVING_BACKWARD",
                EngineState.TramDecelerationBackward => "TRAM_DECELERATION_BACKWARD",
                _ => throw new Exception("Cannot convert such state to ushort"),
            };
        }

        public static EngineState GetStateFromUshort(ushort value)
        {
            return value switch
            {
                0 => EngineState.PowerOn, // 0 Неопределенное состояние АИН (после включения/рестарта)
                1 => EngineState.WaitStart, // 1 (3 сек) Завершение переходных процессов в АИН, Ожидание пульта.
                2 => EngineState.WaitLinkAin1, // 2 Ожидание связи с АИН1 (с ведущим)
                3 => EngineState.TestImcw, // 3 Проверка ролей АИН.
                4 => EngineState.PreResetErrors, // 4 Подготовка к Сбросу аварий АИН3,2,1
                5 => EngineState.ResetErrors, // 5 Сброс аварий АИН2,3,1 (ведущий - последний!).
                6 => EngineState.NotReady, // 6 НЕ Готов к запуску двигателя.
                7 => EngineState.ReadyToSwitchOn, //7
                8 => EngineState.ReadyRun, //8 Готов к запуску двигателя.

                9 => EngineState.EnableOperation,
                10 => EngineState.EnableOutput,
                11 => EngineState.AcceleratorEnable,
                12 => EngineState.OperatingState,

                13 => EngineState.ReadySlave,
                14 => EngineState.DriveSlave,

                15 => EngineState.Off2,
                16 => EngineState.Off3,
                17 => EngineState.Off1,

                18 => EngineState.Inching1,
                19 => EngineState.Inching2,
                20 => EngineState.PostInching,
                21 => EngineState.FaultState,
                22 => EngineState.FaultStateWaitReset,
                23 => EngineState.SwitchOnInhibit,
                24 => EngineState.InhibitOperationActive,
                25 => EngineState.ChopperNotReady,
                26 => EngineState.ChopperRun,
                27 => EngineState.ReadKIs,
                28 => EngineState.ReadMOs,
                29 => EngineState.AciIdentifyStart,
                30 => EngineState.AciIdentifyExecution,
                31 => EngineState.AciIdentifyEnd,

                32 => EngineState.TramStop,
                33 => EngineState.TramMagnetizing,
                34 => EngineState.TramAccelerationForward,
                35 => EngineState.TramMovingForward,
                36 => EngineState.TramDecelerationForward,
                37 => EngineState.TramAccelerationBackward,
                38 => EngineState.TramMovingBackward,
                39 => EngineState.TramDecelerationBackward,

                _ => throw new Exception("Cannot get ushort " + value + " as " + typeof(EngineState).Name),
            };
        }
    }
}