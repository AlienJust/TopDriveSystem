namespace TopDriveSystem.ConfigApp.AppControl.AinsCounter
{
    internal interface IAinsCounter
    {
        int SelectedAinsCount { get; }
        event AinsCountInSystemHasBeenChangedDelegate AinsCountInSystemHasBeenChanged;
    }
}