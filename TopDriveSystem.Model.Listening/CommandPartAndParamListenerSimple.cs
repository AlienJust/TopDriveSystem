using DataAbstractionLevel.Low.PsnConfig.Contracts;
using System;
using System.Collections.Generic;
using TopDriveSystem.CommandSenders.Contracts;

namespace TopDriveSystem.Model.Listening
{
    public sealed class CommandPartAndParamListenerSimple : IParamListener, ICommandPartListener
    {
        private readonly IIOListener _stdListener;
        private readonly IPsnProtocolConfiguration _configuration;

        public event EventHandler<ParameterValueReceivedEventArgs> ValueReceived;
        public event EventHandler<CommandPartReceivedEventArgs> CommandPartReceived;

        public CommandPartAndParamListenerSimple(IIOListener ioListener, IPsnProtocolConfiguration configuration)
        {
            _stdListener = ioListener;
            _configuration = configuration;
            _stdListener.CommandPartHeared += StdListenerCommandPartHeared;
        }

        private void StdListenerCommandPartHeared(object sender, CommandPartHearedEventArgs e)
        {
            foreach (var cmdPartConfig in _configuration.CommandParts)
            {
                try
                {
                    foreach (var param in cmdPartConfig.DefParams)
                    {
                        if (param.DefinedValue != param.GetValue(e.Data, 0))
                        {
                            throw new Exception("Data are not a " + cmdPartConfig.PartName);
                        }
                    }

                    var commandPartReceivedEvent = CommandPartReceived;
                    commandPartReceivedEvent?.Invoke(this, new CommandPartReceivedEventArgs(new CmdPartConfigAndBytesSimple(cmdPartConfig, e.Data)));

                    foreach (var param in cmdPartConfig.VarParams)
                    {
                        RaiseValueReceived(param.Id.IdentyString, param.GetValue(e.Data, 0));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void RaiseValueReceived(string id, double value)
        {
            var valueReceivedEvent = ValueReceived;
            valueReceivedEvent?.Invoke(this, new ParameterValueReceivedEventArgs(id, value));
        }
    }

}