using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TopDriveSystem.ControlApp.ViewModels.Telemetry;

namespace TopDriveSystem.ControlApp.Views.Telemetry
{
    public class Telemetry100View : ReactiveUserControl<ITelemetry100ViewModel>
    {
        public Telemetry100View()
        {
            this.WhenActivated(disposable => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}