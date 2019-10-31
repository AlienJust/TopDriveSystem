using System;

namespace TopDriveSystem.Model.Listening
{
    public interface IParamListener
    {
        event EventHandler<ParameterValueReceivedEventArgs> ValueReceived;
    }
}