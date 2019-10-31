using System;

namespace TopDriveSystem.Model.Listening
{
    public interface ICommandPartListener
    {
        event EventHandler<CommandPartReceivedEventArgs> CommandPartReceived;
    }
}