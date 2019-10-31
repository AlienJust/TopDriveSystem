using System.ComponentModel;
using System.Collections.Generic;

namespace TopDriveSystem.ControlApp.ViewModels.Parameter
{
    public interface IParameterVMsHolder
    {
        IReadOnlyDictionary<string, IParameterViewModel> Parameters { get; }
    }

    public interface IParametrizedViewModel : INotifyPropertyChanged
    {
        IReadOnlyDictionary<string, IParameterViewModel> Parameters { get; }
    }
}