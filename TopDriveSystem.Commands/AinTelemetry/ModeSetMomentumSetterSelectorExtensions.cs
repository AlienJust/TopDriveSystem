using System;

namespace TopDriveSystem.Commands.AinTelemetry
{
    public static class ModeSetMomentumSetterSelectorExtensions
    {
        public static ModeSetMomentumSetterSelector FromInt(int value)
        {
            return value switch
            {
                0 => ModeSetMomentumSetterSelector.SpeedRegulator,
                1 => ModeSetMomentumSetterSelector.ExternalMoment,
                2 => ModeSetMomentumSetterSelector.Summary,
                3 => ModeSetMomentumSetterSelector.Zero,
                _ => throw new Exception("Cannot cast integer value " + value +
                                        " to ModeSetMomentumSetterSelector enum"),
            };
        }
    }
}