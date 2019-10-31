namespace TopDriveSystem.Commands.Rectifier
{
    internal class RectifierTelemetrySimple : IRectifierTelemetry
    {
        public RectifierTelemetrySimple(short voltageInput1, short voltageInput2, short voltageInput3,
            short voltageOutputDc, short current1, short current2, short current3, short temperature)
        {
            VoltageInput1 = voltageInput1;
            VoltageInput2 = voltageInput2;
            VoltageInput3 = voltageInput3;
            VoltageOutputDc = voltageOutputDc;
            Current1 = current1;
            Current2 = current2;
            Current3 = current3;
            Temperature = temperature;
        }

        public short VoltageInput1 { get; }

        public short VoltageInput2 { get; }

        public short VoltageInput3 { get; }

        public short VoltageOutputDc { get; }

        public short Current1 { get; }

        public short Current2 { get; }

        public short Current3 { get; }

        public short Temperature { get; }
    }
}