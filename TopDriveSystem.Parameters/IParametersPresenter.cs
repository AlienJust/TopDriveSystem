using System.Collections.Generic;

namespace TopDriveSystem.Parameters
{
    public interface IParametersPresenter
    {
        Dictionary<string, IParameterDescription> Parameters { get; }
    }
}