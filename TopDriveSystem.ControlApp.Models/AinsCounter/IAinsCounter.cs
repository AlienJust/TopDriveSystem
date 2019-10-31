namespace TopDriveSystem.ControlApp.Models.AinsCounter
{
    public interface IAinsCounter
    {
        int SelectedAinsCount { get; }
        event AinsCountInSystemHasBeenChangedDelegate AinsCountInSystemHasBeenChanged;
    }
}