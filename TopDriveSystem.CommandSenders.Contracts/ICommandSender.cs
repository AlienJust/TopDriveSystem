using System;

namespace TopDriveSystem.CommandSenders.Contracts
{
    public interface ICommandSender : IRrModbusCommandSender, IDisposable, IIOListener
    {
    }
}