using AlienJust.Support.Loggers.Contracts;

namespace TopDriveSystem.ConfigApp.AppControl.LoggerHost
{
    public interface ILoggerRegistrationPoint
    {
        void RegisterLoggegr(ILogger logger);
    }
}