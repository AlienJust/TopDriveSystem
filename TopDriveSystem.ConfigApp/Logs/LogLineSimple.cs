namespace TopDriveSystem.ConfigApp.Logs
{
    internal class LogLineSimple : ILogLine
    {
        public LogLineSimple(string messageText)
        {
            MessageText = messageText;
        }

        public string MessageText { get; }
    }
}