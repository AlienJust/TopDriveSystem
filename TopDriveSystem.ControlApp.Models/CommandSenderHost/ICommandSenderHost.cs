using TopDriveSystem.CommandSenders.Contracts;

namespace TopDriveSystem.ConfigApp.AppControl.CommandSenderHost
{
    public interface ICommandSenderHost
    {
        ICommandSender Sender { get; }
    }
}