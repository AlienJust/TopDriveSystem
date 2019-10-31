using System.Collections.Generic;

namespace TopDriveSystem.Parameters
{
    internal sealed class ParametersPresenterSimple : IParametersPresenter
    {
        public Dictionary<string, IParameterDescription> Parameters { get; set; }
    }
}