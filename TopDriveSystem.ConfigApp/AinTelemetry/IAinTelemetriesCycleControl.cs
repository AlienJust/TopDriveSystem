using System.Windows.Input;

namespace TopDriveSystem.ConfigApp.AinTelemetry
{
    internal interface IAinTelemetriesCycleControl
    {
        ICommand ReadCycleCommand { get; }
        ICommand StopReadingCommand { get; }
    }
}