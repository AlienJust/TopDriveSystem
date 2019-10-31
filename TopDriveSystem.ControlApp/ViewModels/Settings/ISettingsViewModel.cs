using System.ComponentModel;
using ReactiveUI;

namespace TopDriveSystem.ControlApp.ViewModels.Settings
{
    public interface ISettingsViewModel : INotifyPropertyChanged, IRoutableViewModel
    {
        string Name { get; }
    }
}
