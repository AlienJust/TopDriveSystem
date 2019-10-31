using System;

namespace TopDriveSystem.CommandSenders.Contracts
{
    public interface IIOListener
    {
        event EventHandler<CommandPartHearedEventArgs> CommandPartHeared;
    }
}