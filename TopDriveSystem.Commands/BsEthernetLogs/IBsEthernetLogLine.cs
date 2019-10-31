namespace TopDriveSystem.Commands.BsEthernetLogs
{
    public interface IBsEthernetLogLine
    {
        int Number { get; }
        string Content { get; }
    }
}