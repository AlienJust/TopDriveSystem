namespace TopDriveSystem.ControlApp.Models.TargetAddressHost
{
    public interface ITargetAddressHostSettable : ITargetAddressHost
    {
        void SetTargetAddress(byte targetAddress);
    }
}