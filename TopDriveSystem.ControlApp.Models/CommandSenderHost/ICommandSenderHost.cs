using TopDriveSystem.CommandSenders.Contracts;

namespace TopDriveSystem.ControlApp.Models.CommandSenderHost
{
    public interface ICommandSenderHost
    {
        ICommandSender Sender { get; }
    }
}