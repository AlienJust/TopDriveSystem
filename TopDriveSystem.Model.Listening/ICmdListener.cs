namespace TopDriveSystem.Model.Listening
{
    /// <summary>
    /// Type depended interface for command listeners
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICmdListener<out T> : ICmdListenerStd
    {
        /// <summary>
        /// Type depended event when data received
        /// </summary>
        event DataReceivedDelegate<T> DataReceived;
    }
}