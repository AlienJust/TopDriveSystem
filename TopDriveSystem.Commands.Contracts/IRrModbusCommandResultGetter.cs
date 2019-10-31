namespace TopDriveSystem.Commands.Contracts
{
    public interface IRrModbusCommandResultGetter<out T>
    {
        T GetResult(byte[] reply);
    }
}