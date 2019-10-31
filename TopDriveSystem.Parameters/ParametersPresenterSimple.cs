using System.Collections.Generic;

namespace TopDriveSystem.ControlApp.ViewModels.ParameterPresentation
{
    internal sealed class ParametersPresenterSimple : IParametersPresenter
    {
        public Dictionary<string, IParameterDescription> Parameters { get; set; }
    }
}
