using System.Net;
using System.Net.NetworkInformation;

namespace TopDriveSystem.Commands.BsEthernetSettings
{
    public interface IBsEthernetSettings
    {
        PhysicalAddress MacAddress { get; }
        IPAddress IpAddress { get; }
        IPAddress Mask { get; }
        IPAddress Gateway { get; }
        IPAddress DnsServer { get; }
        byte ModbusAddress { get; }
        byte DriveNumber { get; }
        byte AddressCan { get; }
        FriquencyTransformerRole FtRole { get; }
    }
}