namespace TopDriveSystem.ConfigApp.AppControl.ParamLogger
{
    public interface IParamLoggerRegistrationPoint
    {
        void RegisterLoggegr(IParameterLogger logger);
    }
}