using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TopDriveSystem.ControlApp.ViewModels.AinsCountSelection;

namespace TopDriveSystem.ControlApp.Views.AinsCountSelection
{
    public class AinsCountSelectionView : ReactiveUserControl<IAinsCountSelectionViewModel>
    {
        public AinsCountSelectionView()
        {
            this.WhenActivated(disposable => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}