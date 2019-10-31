namespace TopDriveSystem.ConfigApp.AppControl.Cycle
{
    public interface ICycleThreadHolder
    {
        void RegisterAsCyclePart(ICyclePart part);
    }
}