using TopDriveSystem.ControlApp.ViewModels.ParameterPresentation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace TopDriveSystem.ControlApp.ViewModels.Parameter
{
    public interface IParameterSetterViewModel : INotifyPropertyChanged
    {
        IList<ParameterPreselectedValue> CustomValueList { get; }
        double Value { get; set; }

        ICommand SetValue { get; }
    }
}