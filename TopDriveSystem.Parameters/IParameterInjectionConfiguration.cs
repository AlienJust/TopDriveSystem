using System.Collections.Generic;

namespace TopDriveSystem.ControlApp.ViewModels.ParameterPresentation
{
    public interface IParameterInjectionConfiguration
    {
        IList<ParameterPreselectedValue> PreselectedValueList {get;}
        int ZeroBasedParameterNumber { get; }
        ushort GetValue(double value);
    }
}