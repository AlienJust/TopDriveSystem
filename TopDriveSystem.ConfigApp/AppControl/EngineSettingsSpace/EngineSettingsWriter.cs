using System;
using TopDriveSystem.Commands.EngineSettings;
using TopDriveSystem.ConfigApp.AppControl.CommandSenderHost;
using TopDriveSystem.ConfigApp.AppControl.TargetAddressHost;

namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace {
	internal class EngineSettingsWriter : IEngineSettingsWriter {
		private readonly ICommandSenderHost _commandSenderHost;
		private readonly ITargetAddressHost _targerAddressHost;
		private readonly IEngineSettingsReader _engineSettingsReader;

		private readonly TimeSpan _writeSettingsTimeout;

		public EngineSettingsWriter(ICommandSenderHost commandSenderHost, ITargetAddressHost targerAddressHost, IEngineSettingsReader engineSettingsReader) {
			_commandSenderHost = commandSenderHost;
			_targerAddressHost = targerAddressHost;
			_engineSettingsReader = engineSettingsReader;
			_writeSettingsTimeout = TimeSpan.FromMilliseconds(200.0);
		}

		public void WriteSettingsAsync(IEngineSettingsPart settingsPart, Action<Exception> callback) {
			var sender = _commandSenderHost.Sender;
			if (sender == null) throw new NullReferenceException("���� �������� ������ �� ������");

			// ������ ��������� ����� ������� (�� ���������, ��� ��� - �������)
			_engineSettingsReader.ReadSettingsAsync(false, (readSettingsException, engineSettings) => {
				if (readSettingsException != null) {
					callback(new Exception("�� ������� �������� ��������� ���������, �������� ������ ��� ��������������� �� ������ �� BsEthernet", readSettingsException));
					return;
				}

				var engineSettingsMoified = new EngineSettingsWritable(engineSettings);
				engineSettingsMoified.ModifyFromPart(settingsPart);
				var writeAin1SettingsCmd = new WriteEngineSettingsCommand(engineSettingsMoified);
				sender.SendCommandAsync(
					_targerAddressHost.TargetAddress,
					writeAin1SettingsCmd,
					_writeSettingsTimeout, 2,
					(sendException, replyBytes) => {
						if (sendException != null) {
							callback(new Exception("������ �������� ������� ������ �������� ��������� - ��� ������ �� BsEthernet", sendException));
							return;
						}

						// ����� 300 �� ��� ����, ����� ��� ����� �������� ����� ������ � EEPROM,
						// � ����� ��-Ethernet ����� �� �������� �� ���.
						System.Threading.Thread.Sleep(300);

						// �������� ������ �������� ���1 ����� �� ���������� ������
						_engineSettingsReader.ReadSettingsAsync(true, (exceptionOnReReading, engineSettingsReReaded) => {
							if (exceptionOnReReading != null) {
								callback(new Exception("�� ������� ����������������� ������������ ������ �������� ��������� ���� �� ����������� ����������� - ��� ������ �� BsEthernet"));
								return;
							}
							try {
								engineSettingsMoified.CompareSettingsAfterReReading(engineSettingsReReaded, 0);
							}
							catch (Exception compareEx1) {
								callback(new Exception("������ ��� ��������� ������ �������� ���������: " + compareEx1.Message, compareEx1));
								return;
							}
							callback(null);
						});
					});
			});
		}
	}
}