using TopDriveSystem.CommandSenders.Contracts;

namespace TopDriveSystem.ConfigApp.AppControl.CommandSenderHost
{
    internal class CommandSenderHostThreadSafe : ICommandSenderHostSettable
    {
        private readonly object _sendersSync;
        private ICommandSender _sender;

        public CommandSenderHostThreadSafe()
        {
            _sendersSync = new object();
        }

        public void SetCommandSender(ICommandSender sender)
        {
            lock (_sendersSync)
            {
                _sender = sender;
            }
        }

        public ICommandSender Sender
        {
            get
            {
                lock (_sendersSync)
                {
                    return _sender;
                }
            }
        }
    }
}