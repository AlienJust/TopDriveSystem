using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using AlienJust.Support.Concurrent;
using TopDriveSystem.CommandSenders.SerialPortBased;
using TopDriveSystem.CommandSenders.TestCommandSender;
using TopDriveSystem.ControlApp.Models.CommandSenderHost;
using TopDriveSystem.ControlApp.Models.NotifySendingEnabled;
using TopDriveSystem.ControlApp.Models.TargetAddressHost;

namespace TopDriveSystem.ControlApp.Models.DeviceConnection
{
    public sealed class DeviceConnectionModel : IDeviceConnectionModel
    {
        private const string TestPortName = "TECT";
        private readonly ICommandSenderHost _commandSenderHost;
        private readonly ICommandSenderHostSettable _commandSenderHostSettable;
        private readonly INotifySendingEnabledRaisable _notifySendingEnabled;
        private readonly ITargetAddressHost _targetAddressHost;
        private bool _isPortOpened;

        public DeviceConnectionModel(
            ICommandSenderHostSettable commandSenderHostSettable,
            ICommandSenderHost commandSenderHost,
            ITargetAddressHost targetAddressHost,
            INotifySendingEnabledRaisable notifySendingEnabled)
        {
            _commandSenderHostSettable = commandSenderHostSettable;
            _commandSenderHost = commandSenderHost;
            _targetAddressHost = targetAddressHost;
            _notifySendingEnabled = notifySendingEnabled;
        }

        public IReadOnlyList<string> GetPortsAvailable()
        {
            var ports = new List<string>();
            ports.AddRange(SerialPort.GetPortNames());
            ports.Add(TestPortName);
            return ports;
        }

        public void OpenPort(string selectedPortName)
        {
            // must be called only from UI
            try
            {
                if (_isPortOpened) ClosePort();
                //_logger.Log("Открытие порта " + _selectedComName + "...");

                if (selectedPortName == TestPortName)
                {
                    var backWorker =
                        new SingleThreadedRelayQueueWorkerProceedAllItemsBeforeStopNoLog<Action>("NbBackWorker",
                            a => a(), ThreadPriority.BelowNormal, true, null);


                    var sender = new NothingBasedCommandSender(backWorker, backWorker);

                    _commandSenderHostSettable.SetCommandSender(sender);
                }
                else
                {
                    var backWorker =
                        new SingleThreadedRelayQueueWorkerProceedAllItemsBeforeStopNoLog<Action>("SerialPortBackWorker",
                            a => a(), ThreadPriority.BelowNormal, true, null);
                    var sender = new SerialPortBasedCommandSender(backWorker, backWorker, selectedPortName);

                    _commandSenderHostSettable.SetCommandSender(sender);
                }


                _isPortOpened = true;
                //OpenPortCommand.RaiseCanExecuteChanged();
                //ClosePortCommand.RaiseCanExecuteChanged();
                //_logger.Log("Порт " + _selectedComName + " открыт");

                _notifySendingEnabled.SetIsSendingEnabledAndRaiseChange(true);
            }
            catch (Exception ex)
            {
                //_logger.Log("Не удалось открыть порт " + _selectedComName + ". " + ex.Message);
            }
        }

        public void ClosePort()
        {
            try
            {
                _notifySendingEnabled.SetIsSendingEnabledAndRaiseChange(false);
                var currentSender = _commandSenderHost.Sender;
                //_logger.Log("Закрытие ранее открытого порта " + currentSender + "...");

                // Вызов SilentSender.EndWork не производится!
                currentSender.Dispose();
                _commandSenderHostSettable.SetCommandSender(null);

                _isPortOpened = false;
                //OpenPortCommand.RaiseCanExecuteChanged();
                //ClosePortCommand.RaiseCanExecuteChanged();
                //_logger.Log("Ранее открытый порт " + currentSender + " закрыт");
            }
            catch (Exception ex)
            {
                //_logger.Log("Не удалось закрыть открытый ранее порт. " + ex.Message);
            }
        }
    }
}