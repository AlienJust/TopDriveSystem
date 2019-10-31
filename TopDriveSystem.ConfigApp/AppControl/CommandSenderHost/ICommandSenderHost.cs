using TopDriveSystem.CommandSenders.Contracts;

namespace TopDriveSystem.ConfigApp.AppControl.CommandSenderHost {
	internal interface ICommandSenderHost {
		ICommandSender Sender { get; }
	}
}