using TopDriveSystem.CommandSenders.Contracts;

namespace TopDriveSystem.ConfigApp.AppControl.CommandSenderHost {
	internal interface ICommandSenderHostSettable : ICommandSenderHost {
        void SetCommandSender(ICommandSender sender);
		
	}
}