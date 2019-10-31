using System;

namespace TopDriveSystem.CommandSenders.Contracts
{
    public class CommandPartHearedEventArgs : EventArgs
    {
        public CommandPartHearedEventArgs(byte address, byte commandCode, byte[] data)
        {
            Address = address;
            CommandCode = commandCode;
            Data = data;
        }

        public byte Address { get; }
        public byte CommandCode { get; }
        public byte[] Data { get; }
    }
}