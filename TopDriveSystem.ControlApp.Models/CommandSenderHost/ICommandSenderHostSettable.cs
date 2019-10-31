using TopDriveSystem.CommandSenders.Contracts;

namespace TopDriveSystem.ConfigApp.AppControl.CommandSenderHost
{
    public interface ICommandSenderHostSettable : ICommandSenderHost
    {
        void SetCommandSender(ICommandSender sender);
    }
}