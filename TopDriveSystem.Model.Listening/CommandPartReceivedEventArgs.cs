using System;

namespace TopDriveSystem.Model.Listening
{
    public sealed class CommandPartReceivedEventArgs : EventArgs
    {
        public CommandPartReceivedEventArgs(ICmdPartConfigAndBytes data)
        {
            Data = data;
        }

        public ICmdPartConfigAndBytes Data { get; }
    }
}