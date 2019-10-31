using DataAbstractionLevel.Low.PsnConfig.Contracts;

namespace TopDriveSystem.Model.Listening
{
    public interface ICmdPartConfigAndBytes
    {
        IPsnProtocolCommandPartConfiguration CmdPartConfig { get; }
        byte[] DataBytes { get; }
    }
}