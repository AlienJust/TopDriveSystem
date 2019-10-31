namespace TopDriveSystem.Commands.Contracts
{
    public interface IRrModbusCommandWithTestReply
    {
        byte[] GetTestReply();
    }
}