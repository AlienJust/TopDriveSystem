namespace TopDriveSystem.ControlApp.Models.Cycle
{
    public interface ICyclePart
    {
        bool Cancel { get; }
        void InCycleAction();
    }
}