namespace TopDriveSystem.ControlApp.Models.Cycle
{
    public interface ICycleThreadHolder
    {
        void RegisterAsCyclePart(ICyclePart part);
    }
}