using System.Collections.Generic;

namespace TopDriveSystem.Parameters
{
    public interface IParameterInjectionConfiguration
    {
        IList<ParameterPreselectedValue> PreselectedValueList { get; }
        int ZeroBasedParameterNumber { get; }
        ushort GetValue(double value);
    }
}