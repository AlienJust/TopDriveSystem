using System;
using TopDriveSystem.CommandSenders.Contracts;

namespace TopDriveSystem.ControlApp.Models.CommandSenderHost
{
    public sealed class CommandSenderHostThreadSafe : ICommandSenderHostSettable, IIOListener
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
                if (_sender != null) _sender.CommandPartHeared -= SenderCommandPartHeared;
                _sender = sender;
                _sender.CommandPartHeared += SenderCommandPartHeared;
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

        public event EventHandler<CommandPartHearedEventArgs> CommandPartHeared;

        private void SenderCommandPartHeared(object sender, CommandPartHearedEventArgs e)
        {
            var heared = CommandPartHeared;
            heared?.Invoke(this, e);
        }
    }
}