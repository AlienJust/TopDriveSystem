using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace TopDriveSystem.ControlApp.ViewModels.DeviceConnection
{
    public interface IDeviceConnectionViewModel : INotifyPropertyChanged
    {
        ICommand RefreshSerialPortsList { get; }

        IReadOnlyList<string> SerialPortsList { get; }

        string SelectedSerialPortName { get; set; }

        ICommand Connect { get; }

        ICommand Disconnect { get; }
    }
}
