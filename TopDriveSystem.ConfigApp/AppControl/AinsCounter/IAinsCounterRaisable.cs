namespace TopDriveSystem.ConfigApp.AppControl.AinsCounter
{
    internal interface IAinsCounterRaisable : IAinsCounter
    {
        void SetAinsCountAndRaiseChange(int ainsCount);
    }
}