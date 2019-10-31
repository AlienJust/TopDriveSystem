using System;
using System.Threading;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Numeric;
using TopDriveSystem.Commands.Contracts;
using TopDriveSystem.CommandSenders.Contracts;

namespace TopDriveSystem.CommandSenders.TestCommandSender
{
    public class NothingBasedCommandSender : ICommandSender
    {
        private readonly IWorker<Action> _backWorker;
        private readonly IStoppableWorker _backWorkerStoppable;

        public event EventHandler<CommandPartHearedEventArgs> CommandPartHeared;

        //private readonly IMultiLoggerWithStackTrace<int> _debugLogger;
        //private readonly IThreadNotifier _uiNotifier;

        public NothingBasedCommandSender(IWorker<Action> backWorker, IStoppableWorker stoppableBackWorker)
        {
            //_debugLogger = debugLogger;
            //_uiNotifier = uiNotifier;
            _backWorker = backWorker;
            _backWorkerStoppable = stoppableBackWorker;
        }

        public void SendCommandAsync(byte address, IRrModbusCommandWithReply command, TimeSpan timeout,
            int maxAttemptsCount, Action<Exception, byte[]> onComplete)
        {
            _backWorker.AddWork(() =>
            {
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

                    Thread.Sleep(TimeSpan.FromMilliseconds(timeout.TotalMilliseconds / 10.0)); // 1/10 of timeout waiting :)
                    Exception exception = null;
                    byte[] replyBytes;
                    try
                    {
                        if (command is IRrModbusCommandWithTestReply testCmd)
                        {
                            replyBytes = testCmd.GetTestReply();
                            var resultBytes = new byte[replyBytes.Length + 4];
                            resultBytes[0] = address;
                            resultBytes[1] = command.CommandCode;
                            replyBytes.CopyTo(resultBytes, 2);
                            var resultCrc = MathExtensions.GetCrc16FromArray(resultBytes, 0, resultBytes.Length - 2);
                            resultBytes[resultBytes.Length - 2] = resultCrc.Low;
                            resultBytes[resultBytes.Length - 1] = resultCrc.High;

                            RaiseCommandHeared(address, command.CommandCode, resultBytes);
                        }
                        else throw new Exception("Cannot cast command to IRrModbusCommandWithTestReply");
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                        replyBytes = null;
                    }

                    onComplete(exception, replyBytes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
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
            //_debugLogger.GetLogger(1).Log("EndWork called", new StackTrace(true));
            _backWorkerStoppable.StopAsync();
            //_debugLogger.GetLogger(1).Log("backworker stopasync was called", new StackTrace(true));

            _backWorkerStoppable.WaitStopComplete();
            //_debugLogger.GetLogger(1).Log("backworker has been stopped", new StackTrace(true));
        }

        public override string ToString()
        {
            return "Test";
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
        // ~NothingBasedCommandSender()
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