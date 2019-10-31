namespace TopDriveSystem.Commands.Contracts
{
    public interface IRrModbusCommandWithReply : IRrModbusCommand
    {
        int ReplyLength { get; }
    }
}