using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TopDriveSystem.ControlApp.ViewModels.Parameter;

namespace TopDriveSystem.ControlApp.Views.Parameter
{
    public class ParameterView : ReactiveUserControl<IParameterViewModel>
    {
        public ParameterView()
        {
            this.WhenActivated(disposable => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}