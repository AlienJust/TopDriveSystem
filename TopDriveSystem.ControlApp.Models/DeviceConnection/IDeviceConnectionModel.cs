using System.Collections.Generic;

namespace TopDriveSystem.ControlApp.Models.DeviceConnection
{
    public interface IDeviceConnectionModel
    {
        IReadOnlyList<string> GetPortsAvailable();

        void ClosePort();

        void OpenPort(string portName);
    }
}
