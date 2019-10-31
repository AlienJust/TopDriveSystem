using ReactiveUI;

namespace TopDriveSystem.ControlApp.ViewModels.Parameter
{
    internal sealed class ParameterViewModelSimple : ReactiveObject, IParameterViewModel
    {
        public ParameterViewModelSimple(string customName, string parameterNameFromConfiguration,
            IParameterGetterViewModel getter, IParameterSetterViewModel setter)
        {
            Name = customName ?? parameterNameFromConfiguration;
            Getter = getter;
            Setter = setter;
        }

        public string Name { get; }

        public IParameterGetterViewModel Getter { get; }

        public IParameterSetterViewModel Setter { get; }
    }
}