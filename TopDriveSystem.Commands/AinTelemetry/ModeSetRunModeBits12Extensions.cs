using System;

namespace TopDriveSystem.Commands.AinTelemetry
{
    public static class ModeSetRunModeBits12Extensions
    {
        public static ModeSetRunModeBits12 FromInt(int value)
        {
            return value switch
            {
                0 => ModeSetRunModeBits12.Freewell,
                1 => ModeSetRunModeBits12.Traction,
                2 => ModeSetRunModeBits12.Unknown2,
                3 => ModeSetRunModeBits12.Unknown3,
                _ => throw new Exception("Cannot cast integer value " + value + " to ModeSetRunModeBits12 enum"),
            };
        }
    }
}