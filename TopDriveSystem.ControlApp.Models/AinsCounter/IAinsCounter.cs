namespace TopDriveSystem.ConfigApp.AppControl.AinsCounter
{
    public interface IAinsCounter
    {
        int SelectedAinsCount { get; }
        event AinsCountInSystemHasBeenChangedDelegate AinsCountInSystemHasBeenChanged;
    }
}