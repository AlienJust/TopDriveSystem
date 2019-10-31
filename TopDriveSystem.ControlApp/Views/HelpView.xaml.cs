using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TopDriveSystem.ControlApp.ViewModels;

namespace TopDriveSystem.ControlApp.Views
{
    public class HelpView : ReactiveUserControl<HelpViewModel>
    {
        public HelpView()
        {
            this.WhenActivated(disposable => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}

