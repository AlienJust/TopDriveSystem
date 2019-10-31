using System.Net;
using System.Net.NetworkInformation;

namespace TopDriveSystem.Commands.BsEthernetSettings
{
    public class BsEthernetSettingsSimple : IBsEthernetSettings
    {
        public BsEthernetSettingsSimple(
            PhysicalAddress macAddress,
            IPAddress ipAddress,
            IPAddress mask,
            IPAddress gateway,
            IPAddress dnsServer,
            byte modbusAddress,
            byte driveNumber,
            byte addressCan,
            FriquencyTransformerRole ftRole)
        {
            IpAddress = ipAddress;
            Mask = mask;
            Gateway = gateway;
            DnsServer = dnsServer;
            MacAddress = macAddress;
            ModbusAddress = modbusAddress;
            DriveNumber = driveNumber;
            AddressCan = addressCan;
            FtRole = ftRole;
        }

        public PhysicalAddress MacAddress { get; }

        public IPAddress IpAddress { get; }

        public IPAddress Mask { get; }

        public IPAddress Gateway { get; }

        public IPAddress DnsServer { get; }

        public byte ModbusAddress { get; }

        public byte DriveNumber { get; }

        public byte AddressCan { get; }

        public FriquencyTransformerRole FtRole { get; }
    }
}