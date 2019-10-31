using AlienJust.Support.Mvvm;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.Chart
{
    internal class WindowChartViewModel : ViewModelBase
    {
        public WindowChartViewModel(ChartViewModel chartVm)
        {
            ChartVm = chartVm;
        }

        public ChartViewModel ChartVm { get; }
    }
}