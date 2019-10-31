using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TopDriveSystem.ControlApp.ViewModels.Settings;

namespace TopDriveSystem.ControlApp.Views.Settings
{
    public class SettingsView : ReactiveUserControl<ISettingsViewModel>
    {
        public SettingsView()
        {
            this.WhenActivated(disposable => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
