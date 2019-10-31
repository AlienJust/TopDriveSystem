namespace TopDriveSystem.Commands.RtuModbus.Telemetry04
{
    internal class Telemetry04Simple : ITelemetry04
    {
        public short Pver { get; set; }
        public ushort PvDate { get; set; }
        public short BsVer { get; set; }
    }
}