using DataAbstractionLevel.Low.PsnConfig.Contracts;

namespace TopDriveSystem.Model.Listening
{
    internal sealed class CmdPartConfigAndBytesSimple : ICmdPartConfigAndBytes
    {
        public CmdPartConfigAndBytesSimple(IPsnProtocolCommandPartConfiguration config, byte[] bytes)
        {
            CmdPartConfig = config;
            DataBytes = bytes;
        }

        public IPsnProtocolCommandPartConfiguration CmdPartConfig { get; }
        public byte[] DataBytes { get; }
    }
}