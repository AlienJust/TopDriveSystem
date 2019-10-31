namespace TopDriveSystem.ConfigApp.AinTelemetry
{
    internal class AinTelemetryExpandedViewModel
    {
        public AinTelemetryExpandedViewModel(string ainName, AinTelemetryViewModel ainTelemetryVm)
        {
            AinName = ainName;
            AinTelemetryVm = ainTelemetryVm;
        }

        public string AinName { get; set; }

        public AinTelemetryViewModel AinTelemetryVm { get; set; }
    }
}