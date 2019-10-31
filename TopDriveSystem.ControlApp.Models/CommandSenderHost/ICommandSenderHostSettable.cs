using TopDriveSystem.CommandSenders.Contracts;

namespace TopDriveSystem.ControlApp.Models.CommandSenderHost
{
    public interface ICommandSenderHostSettable : ICommandSenderHost
    {
        void SetCommandSender(ICommandSender sender);
    }
}