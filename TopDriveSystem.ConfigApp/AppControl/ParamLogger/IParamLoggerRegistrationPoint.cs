using AlienJust.Support.Loggers.Contracts;
using TopDriveSystem.ConfigApp.AppControl.LoggerHost;
using TopDriveSystem.ConfigApp.LookedLikeAbb;

namespace TopDriveSystem.ConfigApp.AppControl.ParamLogger {
	internal interface IParamLoggerRegistrationPoint {
		void RegisterLoggegr(IParameterLogger logger);
	}
}