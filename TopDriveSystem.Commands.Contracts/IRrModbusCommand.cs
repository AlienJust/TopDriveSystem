namespace TopDriveSystem.Commands.Contracts
{
    public interface IRrModbusCommand
    {
        byte CommandCode { get; }
        string Name { get; }
        byte[] Serialize();
    }
}