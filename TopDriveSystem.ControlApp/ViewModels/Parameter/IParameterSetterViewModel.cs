using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using TopDriveSystem.Parameters;

namespace TopDriveSystem.ControlApp.ViewModels.Parameter
{
    public interface IParameterSetterViewModel : INotifyPropertyChanged
    {
        IList<ParameterPreselectedValue> CustomValueList { get; }
        double Value { get; set; }

        ICommand SetValue { get; }
    }
}