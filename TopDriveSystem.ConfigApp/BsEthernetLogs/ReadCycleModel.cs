using System;
using System.Threading;
using TopDriveSystem.Commands.BsEthernetLogs;
using TopDriveSystem.ControlApp.Models.CommandSenderHost;
using TopDriveSystem.ControlApp.Models.NotifySendingEnabled;
using TopDriveSystem.ControlApp.Models.TargetAddressHost;

namespace TopDriveSystem.ConfigApp.BsEthernetLogs
{
    internal class ReadCycleModel : IReadCycleModel
    {
        private readonly Thread _backgroundThread;
        private readonly ICommandSenderHost _commandSenderHost;
        private readonly ManualResetEventSlim _conditionsChangedWaiter;
        private readonly INotifySendingEnabled _notifySendingEnabled;
        private readonly object _syncEnabled;

        private readonly object _syncStop;
        private readonly ITargetAddressHost _targetAddressHost;
        private bool _isReadCycleEnabled;

        private bool _isStopFlagRaised;

        public ReadCycleModel(ICommandSenderHost commandSenderHost,
            ITargetAddressHost targetAddressHost, INotifySendingEnabled notifySendingEnabled)
        {
            _commandSenderHost = commandSenderHost;
            _targetAddressHost = targetAddressHost;
            _notifySendingEnabled = notifySendingEnabled;
            _notifySendingEnabled.SendingEnabledChanged += NotifySendingEnabledOnSendingEnabledChanged;

            _syncStop = new object();
            _syncEnabled = new object();

            _isReadCycleEnabled = true;
            _isStopFlagRaised = false;
            _conditionsChangedWaiter = new ManualResetEventSlim(false);

            _backgroundThread = new Thread(ReadLogsCycle) {Priority = ThreadPriority.BelowNormal, IsBackground = true};
            _backgroundThread.Start();
        }

        private bool IsStopFlagRaised
        {
            get
            {
                lock (_syncStop)
                {
                    return _isStopFlagRaised;
                }
            }
            set
            {
                lock (_syncStop)
                {
                    if (value)
                    {
                        _isStopFlagRaised = true;
                        _conditionsChangedWaiter.Set();
                    }
                }
            }
        }

        public void StopBackgroundThreadAndWaitForIt()
        {
            IsStopFlagRaised = true;
            _backgroundThread.Join();
        }

        public bool IsReadCycleEnabled
        {
            get
            {
                lock (_syncEnabled)
                {
                    return _isReadCycleEnabled;
                }
            }
            set
            {
                lock (_syncEnabled)
                {
                    _isReadCycleEnabled = value;

                    if (value) _conditionsChangedWaiter.Set();
                    else _conditionsChangedWaiter.Reset();
                }
            }
        }

        public event IcAnotherLogLineWasReadedOrNot AnotherLogLineWasReaded;

        private void NotifySendingEnabledOnSendingEnabledChanged(bool isSendingEnabled)
        {
            if (isSendingEnabled) _conditionsChangedWaiter.Set();
            else _conditionsChangedWaiter.Reset();
        }

        private void ReadLogsCycle()
        {
            var cmd = new ReadBsEthernetLogLineCommand();
            var timeout = TimeSpan.FromMilliseconds(100);
            while (!IsStopFlagRaised)
            {
                _conditionsChangedWaiter.Wait();
                if (!IsStopFlagRaised && IsReadCycleEnabled && _notifySendingEnabled.IsSendingEnabled)
                {
                    _commandSenderHost.Sender.SendCommandAsync(_targetAddressHost.TargetAddress, cmd,
                        timeout, 2,
                        (exception, bytes) =>
                        {
                            try
                            {
                                if (exception != null) throw exception;
                                try
                                {
                                    RaiseAnotherLogLineWasReaded(cmd.GetResult(bytes));
                                }
                                catch
                                {
                                    // TODO: may be log? It is program error, user callback is broken!
                                }
                            }
                            catch
                            {
                                RaiseAnotherLogLineWasReaded(null);
                            }
                        });
                    Thread.Sleep(500);
                }
            }
        }

        private void RaiseAnotherLogLineWasReaded(IBsEthernetLogLine logLine)
        {
            try
            {
                AnotherLogLineWasReaded?.Invoke(logLine);
            }
            catch
            {
                // TODO: may be log? It is program error, user callback is broken!
            }
        }
    }
}