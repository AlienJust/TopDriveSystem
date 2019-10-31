using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;
using ReactiveUI;
using TopDriveSystem.ControlApp.Models.DeviceConnection;

namespace TopDriveSystem.ControlApp.ViewModels.DeviceConnection
{
    public class DeviceConnectionViewModel : ReactiveObject, IDeviceConnectionViewModel
    {
        private readonly ReactiveCommand<Unit, Unit> _connect;
        private readonly IDeviceConnectionModel _deviceConnectionModel;
        private readonly ReactiveCommand<Unit, Unit> _disconnect;
        private readonly ReactiveCommand<Unit, Unit> _refreshSerialPortsList;
        private bool _isConnected;

        private string _selectedSerialPortName;
        private IReadOnlyList<string> _serialPortsList;

        public DeviceConnectionViewModel(IDeviceConnectionModel deviceConnectionModel)
        {
            _deviceConnectionModel = deviceConnectionModel;

            _serialPortsList = _deviceConnectionModel.GetPortsAvailable();
            _selectedSerialPortName = _serialPortsList.FirstOrDefault();

            var canConnect = this
                .WhenAnyValue(x => x.IsConnected)
                .Select(isConnected => !isConnected);

            var canDisconnect = this
                .WhenAnyValue(x => x.IsConnected)
                .Select(isConnected => isConnected);


            _connect = ReactiveCommand.Create(() =>
            {
                _deviceConnectionModel.OpenPort(SelectedSerialPortName);
                IsConnected = true;
            }, canConnect);

            _disconnect = ReactiveCommand.Create(() =>
            {
                _deviceConnectionModel.ClosePort();
                IsConnected = false;
            }, canDisconnect);


            _refreshSerialPortsList = ReactiveCommand.Create(() =>
            {
                SerialPortsList = _deviceConnectionModel.GetPortsAvailable();
            });
        }


        public bool IsConnected
        {
            get => _isConnected;
            set => this.RaiseAndSetIfChanged(ref _isConnected, value);
        }


        public ICommand Connect => _connect;

        public ICommand RefreshSerialPortsList => _refreshSerialPortsList;

        public IReadOnlyList<string> SerialPortsList
        {
            get => _serialPortsList;
            set => this.RaiseAndSetIfChanged(ref _serialPortsList, value);
        }


        [DataMember]
        public string SelectedSerialPortName
        {
            get => _selectedSerialPortName;
            set => this.RaiseAndSetIfChanged(ref _selectedSerialPortName, value);
        }


        public ICommand Disconnect => _disconnect;
    }
}