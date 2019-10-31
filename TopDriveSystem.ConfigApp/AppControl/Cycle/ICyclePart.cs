namespace TopDriveSystem.ConfigApp.AppControl.Cycle {
	internal interface ICyclePart {
		void InCycleAction();
		bool Cancel { get; }
	}
}