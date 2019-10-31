using System;
using System.Threading;
using AlienJust.Support.Concurrent;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers.Contracts;
using TopDriveSystem.Commands.EngineSettings;
using TopDriveSystem.ConfigApp.AppControl.CommandSenderHost;
using TopDriveSystem.ConfigApp.AppControl.TargetAddressHost;

namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace
{
    internal class EngineSettingsReader : IEngineSettingsReader, IEngineSettingsReadNotifyRaisable
    {
        private readonly ICommandSenderHost _commandSenderHost;
        private readonly ILogger _logger;
        private readonly IWorker<Action> _notifyWorker;
        private readonly TimeSpan _readSettingsTimeout;
        private readonly IEngineSettingsStorageSettable _settingsStorageSettable;
        private readonly ITargetAddressHost _targerAddressHost;

        public EngineSettingsReader(ICommandSenderHost commandSenderHost, ITargetAddressHost targerAddressHost,
            ILogger logger,
            IEngineSettingsStorageSettable settingsStorageSettable, IMultiLoggerWithStackTrace<int> debugLogger)
        {
            _commandSenderHost = commandSenderHost;
            _targerAddressHost = targerAddressHost;
            _logger = logger;
            _settingsStorageSettable = settingsStorageSettable;
            _readSettingsTimeout = TimeSpan.FromMilliseconds(200.0);
            _notifyWorker = new SingleThreadedRelayQueueWorker<Action>("EngineSettingsReaderNotify", a => a(),
                ThreadPriority.BelowNormal, true, null, debugLogger.GetLogger(0));
        }

        public void ReadSettingsAsync(bool forceRead, Action<Exception, IEngineSettings> callback)
        {
            // ������ �������� ������������ ������ ��� ������� ���

            if (forceRead == false)
            {
                var settings = _settingsStorageSettable.EngineSettings;
                if (settings != null)
                {
                    _notifyWorker.AddWork(() => callback.Invoke(null, settings));
                    return;
                }
            }

            var sender = _commandSenderHost.Sender;
            if (sender == null) throw new NullReferenceException("���� �������� ������ �� ������");

            var readSettingsCmd = new ReadEngineSettingsCommand();

            _notifyWorker.AddWork(FireEventEngineSettingsReadStarted);
            _logger.Log("������ �������� ���������...");
            sender.SendCommandAsync(_targerAddressHost.TargetAddress, readSettingsCmd, _readSettingsTimeout, 2,
                (sendException, replyBytes) =>
                {
                    if (sendException != null)
                    {
                        var errorMessage = "��������� ������ �� ����� ������ ������� ���������";
                        _logger.Log(errorMessage);
                        try
                        {
                            var ex = new Exception(errorMessage, sendException);

                            _notifyWorker.AddWork(() => callback.Invoke(ex, null));
                            _notifyWorker.AddWork(() => FireEventEngineSettingsReadComplete(ex, null));
                            _notifyWorker.AddWork(() => _settingsStorageSettable.SetSettings(null));
                        }
                        catch (Exception)
                        {
                            _logger.Log(
                                "�� ������� ��������� �������� ����� ����� ���������� ������ �������� ��������� (���� �� ������� ��null��� � ���������)");
                        }

                        return;
                    }

                    try
                    {
                        var result = readSettingsCmd.GetResult(replyBytes);
                        try
                        {
                            _notifyWorker.AddWork(() => callback.Invoke(null, result));
                            _notifyWorker.AddWork(() => FireEventEngineSettingsReadComplete(null, result));
                            _notifyWorker.AddWork(() => _settingsStorageSettable.SetSettings(result));
                            _logger.Log("��������� ��������� ������� ���������");
                        }
                        catch
                        {
                            _logger.Log(
                                "�� ������� ��������� �������� ����� ����� ��������� ������ �������� ��������� (���� �� ������� ��������� ��������� � ���������)");
                        }
                    }
                    catch (Exception resultGetException)
                    {
                        var errorMessage = "������ �� ����� ������� ������ �� ������� ������ �������� ���������: " +
                                           resultGetException.Message;
                        _logger.Log(errorMessage);
                        try
                        {
                            var ex = new Exception(errorMessage, resultGetException);
                            _notifyWorker.AddWork(() => callback.Invoke(ex, null));
                            _notifyWorker.AddWork(() => FireEventEngineSettingsReadComplete(ex, null));
                            _notifyWorker.AddWork(() => _settingsStorageSettable.SetSettings(null));
                        }
                        catch
                        {
                            _logger.Log(
                                "�� ������� ��������� �������� ����� ����� ���������� �������� �������� ��������� (���� �� ������� ��null��� � ���������)");
                        }
                    }
                });
        }

        public event EngineSettingsReadCompleteDelegate EngineSettingsReadComplete;
        public event EngineSettingsReadStartedDelegate EngineSettingsReadStarted;

        public void RaiseEngineSettingsReadStarted()
        {
            _notifyWorker.AddWork(FireEventEngineSettingsReadStarted);
        }

        public void RaiseEngineSettingsReadComplete(Exception innerException, IEngineSettings settings)
        {
            _notifyWorker.AddWork(() => FireEventEngineSettingsReadComplete(innerException, settings));
        }

        private void FireEventEngineSettingsReadComplete(Exception innerException, IEngineSettings settings)
        {
            var eve = EngineSettingsReadComplete;
            eve?.Invoke(innerException, settings);
        }

        private void FireEventEngineSettingsReadStarted()
        {
            var eve = EngineSettingsReadStarted;
            eve?.Invoke();
        }
    }
}