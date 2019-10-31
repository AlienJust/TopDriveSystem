using System.Collections.Generic;

namespace TopDriveSystem.ControlApp.ViewModels.ParameterPresentation
{
    public interface IParametersPresenter
    {
        Dictionary<string, IParameterDescription> Parameters { get; }
    }
}
