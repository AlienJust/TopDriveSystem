namespace TopDriveSystem.ControlApp.Models.AinsCounter
{
    public interface IAinsCounterRaisable : IAinsCounter
    {
        void SetAinsCountAndRaiseChange(int ainsCount);
    }
}