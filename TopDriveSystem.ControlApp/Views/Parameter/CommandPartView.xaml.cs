using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TopDriveSystem.ControlApp.ViewModels.Parameter;

namespace TopDriveSystem.ControlApp.Views.Parameter
{
    public class CommandPartView : ReactiveUserControl<ICommandPartViewModel>
    {
        public CommandPartView()
        {
            this.WhenActivated(disposable => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}