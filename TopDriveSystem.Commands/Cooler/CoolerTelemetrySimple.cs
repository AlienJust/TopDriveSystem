namespace TopDriveSystem.Commands.Cooler
{
    internal class CoolerTelemetrySimple : ICoolerTelemetry
    {
        public CoolerTelemetrySimple(ushort diagnostic, short coolingLiquidPressure, short fanSpeed,
            short coolingLiquidTemperature, ushort reserve1, ushort reserve2)
        {
            Diagnostic = diagnostic;
            CoolingLiquidPressure = coolingLiquidPressure;
            FanSpeed = fanSpeed;
            CoolingLiquidTemperature = coolingLiquidTemperature;
            Reserve1 = reserve1;
            Reserve2 = reserve2;
        }

        public ushort Diagnostic { get; }

        public short CoolingLiquidPressure { get; }

        public short FanSpeed { get; }

        public short CoolingLiquidTemperature { get; }

        public ushort Reserve1 { get; }

        public ushort Reserve2 { get; }
    }
}