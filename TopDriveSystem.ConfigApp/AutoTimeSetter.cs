using System;
using AlienJust.Support.Loggers.Contracts;
using TopDriveSystem.Commands.AinCommand;
using TopDriveSystem.Commands.SetTime;
using TopDriveSystem.ConfigApp.AppControl.CommandSenderHost;
using TopDriveSystem.ConfigApp.AppControl.NotifySendingEnabled;
using TopDriveSystem.ConfigApp.AppControl.TargetAddressHost;

namespace TopDriveSystem.ConfigApp {
	internal class AutoTimeSetter {
		private readonly ICommandSenderHost _commandSenderHost;
		private readonly INotifySendingEnabled _notifySendingEnabled;
		private readonly ITargetAddressHost _targetAddressHost;
		private readonly ILogger _logger;

		public AutoTimeSetter(ICommandSenderHost commandSenderHost, INotifySendingEnabled notifySendingEnabled, ITargetAddressHost targetAddressHost, ILogger logger) {
			_commandSenderHost = commandSenderHost;
			_notifySendingEnabled = notifySendingEnabled;
			_targetAddressHost = targetAddressHost;
			_logger = logger;
			_notifySendingEnabled.SendingEnabledChanged += NotifySendingEnabledOnSendingEnabledChanged;
		}

		private void NotifySendingEnabledOnSendingEnabledChanged(bool isSendingEnabled) {
			if (isSendingEnabled) {
				var time = DateTime.Now;
				var cmd = new SetTimeCommand(time);
				_logger.Log("Отправка команды " + cmd.Name);
				_commandSenderHost.Sender.SendCommandAsync(_targetAddressHost.TargetAddress, cmd, TimeSpan.FromMilliseconds(200.0), 2, (e, r) => {
					try {
						if (e != null) throw new Exception("Не удалось установить время, ошибка связи", e);
						cmd.GetResult(r);
						_logger.Log("Время БС-Ethernet было установлено в " + time.ToString("yyyy.MM.dd-HH:mm:ss"));
					}
					catch (Exception ex) {
						_logger.Log(ex.Message);
					}
				});
			}
		}
	}
}