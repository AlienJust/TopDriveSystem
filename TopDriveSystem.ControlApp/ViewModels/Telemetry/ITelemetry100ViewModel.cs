using System.ComponentModel;
using ReactiveUI;
using TopDriveSystem.ControlApp.ViewModels.Parameter;

namespace TopDriveSystem.ControlApp.ViewModels.Telemetry
{
    public interface ITelemetry100ViewModel : IParametrizedViewModel, INotifyPropertyChanged, IRoutableViewModel
    {
        string Name { get; }
    }
}
