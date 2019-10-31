using AlienJust.Support.Loggers.Contracts;

namespace TopDriveSystem.ConfigApp.AppControl.LoggerHost {
	internal interface ILoggerRegistrationPoint {
		void RegisterLoggegr(ILogger logger);
	}
}