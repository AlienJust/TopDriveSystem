using System.ComponentModel;

namespace TopDriveSystem.ControlApp.ViewModels.Parameter
{
    public interface IParameterViewModel : INotifyPropertyChanged
    {
        string Name { get; }

        IParameterGetterViewModel Getter { get; }
        IParameterSetterViewModel Setter { get; }
    }
}