namespace TopDriveSystem.ConfigApp.AppControl.ParamLogger
{
    internal interface IParamLoggerRegistrationPoint
    {
        void RegisterLoggegr(IParameterLogger logger);
    }
}