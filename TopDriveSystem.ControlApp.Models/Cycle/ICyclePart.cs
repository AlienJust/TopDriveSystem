namespace TopDriveSystem.ConfigApp.AppControl.Cycle
{
    public interface ICyclePart
    {
        bool Cancel { get; }
        void InCycleAction();
    }
}