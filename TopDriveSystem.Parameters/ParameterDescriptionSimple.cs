namespace TopDriveSystem.ControlApp.ViewModels.ParameterPresentation
{
    internal sealed class ParameterDescriptionSimple : IParameterDescription
    {
        public string Key { get; set; }

        public string Identifier { get; set; }

        public string CustomName { get; set; }

        public IParameterView View { get; set; }

        public IParameterInjectionConfiguration Injection { get; set; }
    }
}