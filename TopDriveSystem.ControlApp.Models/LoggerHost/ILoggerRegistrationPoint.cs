using AlienJust.Support.Loggers.Contracts;

namespace TopDriveSystem.ControlApp.Models.LoggerHost
{
    public interface ILoggerRegistrationPoint
    {
        void RegisterLoggegr(ILogger logger);
    }
}