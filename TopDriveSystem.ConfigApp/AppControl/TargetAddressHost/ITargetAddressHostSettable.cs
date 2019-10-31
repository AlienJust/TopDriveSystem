namespace TopDriveSystem.ConfigApp.AppControl.TargetAddressHost {
	internal interface ITargetAddressHostSettable : ITargetAddressHost {
		void SetTargetAddress(byte targetAddress);
		
	}
}