namespace TopDriveSystem.Model.Listening
{
    /// <summary>
    ///     Defines common functionality for all listeners independent of they types
    /// </summary>
    public interface ICmdListenerStd
    {
        /// <summary>
        ///     Each listener have to receive command
        /// </summary>
        /// <param name="addr">Address for the command</param>
        /// <param name="code">Command code</param>
        /// <param name="data">Data bytes</param>
        void ReceiveCommand(byte addr, byte code, byte[] data);
    }
}