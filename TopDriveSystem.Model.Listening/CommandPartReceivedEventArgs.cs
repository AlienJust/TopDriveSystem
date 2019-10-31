using System;

namespace TopDriveSystem.Model.Listening
{
    public sealed class CommandPartReceivedEventArgs : EventArgs
    {
        public ICmdPartConfigAndBytes Data { get; private set; }

        public CommandPartReceivedEventArgs(ICmdPartConfigAndBytes data) : base()
        {
            Data = data;
        }
    }
}