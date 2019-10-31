namespace TopDriveSystem.ConfigApp.AppControl.TargetAddressHost
{
    public interface ITargetAddressHostSettable : ITargetAddressHost
    {
        void SetTargetAddress(byte targetAddress);
    }
}