namespace TopDriveSystem.ConfigApp.AppControl.AinsCounter
{
    public interface IAinsCounterRaisable : IAinsCounter
    {
        void SetAinsCountAndRaiseChange(int ainsCount);
    }
}