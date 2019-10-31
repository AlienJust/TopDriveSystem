namespace TopDriveSystem.ConfigApp.AppControl.Cycle
{
    internal interface ICyclePart
    {
        bool Cancel { get; }
        void InCycleAction();
    }
}