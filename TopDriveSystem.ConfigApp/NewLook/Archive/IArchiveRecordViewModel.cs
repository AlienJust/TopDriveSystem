namespace TopDriveSystem.ConfigApp.NewLook.Archive
{
    internal interface IArchiveRecordViewModel
    {
        string Time { get; }
        string FaultState { get; }
        string Mcw { get; }
        string Msw { get; }
    }
}