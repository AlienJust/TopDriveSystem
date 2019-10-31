using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TopDriveSystem.ControlApp.ViewModels.DeviceConnection;

namespace TopDriveSystem.ControlApp.Views.DeviceConnection
{
    public class DeviceConnectionView : ReactiveUserControl<IDeviceConnectionViewModel>
    {
        public DeviceConnectionView()
        {
            this.WhenActivated(disposable => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
