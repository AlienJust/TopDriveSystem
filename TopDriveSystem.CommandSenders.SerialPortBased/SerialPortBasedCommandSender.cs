using System;
using System.IO.Ports;
using System.Threading;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Numeric;
using AlienJust.Support.Serial;
using TopDriveSystem.Commands.Contracts;
using TopDriveSystem.CommandSenders.Contracts;

namespace TopDriveSystem.CommandSenders.SerialPortBased
{
    public class SerialPortBasedCommandSender : ICommandSender
    {
        private readonly IWorker<Action> _backWorker;
        private readonly IStoppableWorker _backWorkerStoppable;
        private readonly ISerialPortExtender _portExtender;
        private readonly SerialPort _serialPort;

        public event EventHandler<CommandPartHearedEventArgs> CommandPartHeared;

        public SerialPortBasedCommandSender(IWorker<Action> backWorker, IStoppableWorker stoppableBackWorker, string selectedComName)
        {
            _serialPort = new SerialPort(selectedComName, 115200);
            _serialPort.Open();

            _portExtender = new SerialPortExtenderNoLog(_serialPort);
            _backWorker = backWorker;
            _backWorkerStoppable = stoppableBackWorker;
        }

        public void SendCommandAsync(byte address, IRrModbusCommandWithReply command, TimeSpan timeout,
            int maxAttemptsCount, Action<Exception, byte[]> onComplete)
        {
            _backWorker.AddWork(() =>
            {
                Exception backgroundException = null;
                byte[] resultBytes = null;
                try
                {
                    var cmdBytes = command.Serialize();

                    var sendBytes = new byte[cmdBytes.Length + 4]; // 1 byte address + 2 bytes CRC16
                    sendBytes[0] = address;
                    sendBytes[1] = command.CommandCode;
                    cmdBytes.CopyTo(sendBytes, 2);


                    var sendCrc = MathExtensions.GetCrc16FromArray(sendBytes, 0, sendBytes.Length - 2);
                    sendBytes[sendBytes.Length - 2] = sendCrc.Low;
                    sendBytes[sendBytes.Length - 1] = sendCrc.High;

                    RaiseCommandHeared(address, command.CommandCode, sendBytes);

                    byte[] replyBytes = null;
                    Exception lastException = null;
                    for (var i = 0; i < maxAttemptsCount; ++i)
                        try
                        {
                            _portExtender.WriteBytes(sendBytes, 0, sendBytes.Length);
                            replyBytes = _portExtender.ReadBytes(command.ReplyLength + 4, timeout, true); // + 4 bytes are: addr, cmd, crc, crc
                            lastException = null;
                            break;
                        }
                        catch (Exception ex)
                        {
                            lastException = ex;
                            replyBytes = null;
                        }

                    if (lastException != null) throw lastException;

                    if (replyBytes != null)
                    {
                        // length is checked in port extender
                        if (replyBytes[0] != address) throw new Exception("Address is wrong");
                        if (replyBytes[1] != command.CommandCode)
                            throw new Exception("Command code is wrong (" + replyBytes[1] +
                                                "), assumed the same as it was sended: " + command.CommandCode);
                        var crc = MathExtensions.GetCrc16FromArray(replyBytes, 0, replyBytes.Length - 2);
                        if (crc.Low != replyBytes[replyBytes.Length - 2])
                            throw new Exception("Crc Low byte is wrong, assumed to be 0x" + crc.Low.ToString("x2") +
                                                " (" + crc.Low + " dec)");
                        if (crc.High != replyBytes[replyBytes.Length - 1])
                            throw new Exception("Crc High byte is wrong, assumed to be 0x" + crc.High.ToString("x2") +
                                                " (" + crc.High + " dec)");

                        resultBytes = new byte[replyBytes.Length - 4];
                        for (var i = 2; i < replyBytes.Length - 2; ++i)
                            resultBytes[i - 2] = replyBytes[i];

                        RaiseCommandHeared(address, command.CommandCode, replyBytes);
                    }
                    else
                    {
                        throw new Exception("Внутренняя ошибка алгоритма, обратитесь к разработчикам");
                    }
                }
                catch (Exception ex)
                {
                    backgroundException = ex;
                    resultBytes = null;
                }
                finally
                {
                    onComplete(backgroundException, resultBytes);
                }
            });
        }

        void RaiseCommandHeared(byte address, byte commandCode, byte[] data)
        {
            var eve = CommandPartHeared;
            eve?.Invoke(this, new CommandPartHearedEventArgs(address, commandCode, data));
        }

        private void EndWork()
        {
            //_debugLogger.GetLogger(1).Log("EndWork called", new StackTrace());
            var portCloseWaiter = new ManualResetEvent(false);
            _backWorker.AddWork(() =>
            {
                //_debugLogger.GetLogger(4).Log("Closing port...", new StackTrace());
                _serialPort.Close();
                //_debugLogger.GetLogger(4).Log("Port was closed", new StackTrace());
                portCloseWaiter.Set();
            });
            _backWorkerStoppable.StopAsync();
            _backWorkerStoppable.WaitStopComplete();
            portCloseWaiter.Dispose();
        }

        public override string ToString()
        {
            return _serialPort.PortName;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    EndWork();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SerialPortBasedCommandSender()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}