using AlienJust.Support.Mvvm;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.Chart {
	class WindowChartViewModel :ViewModelBase {
		public WindowChartViewModel(ChartViewModel chartVm) {
			ChartVm = chartVm;
		}

		public ChartViewModel ChartVm { get; }
	}
}
