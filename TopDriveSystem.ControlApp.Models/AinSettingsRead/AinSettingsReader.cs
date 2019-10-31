using System;
using System.Threading;
using AlienJust.Support.Concurrent;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers.Contracts;
using TopDriveSystem.Commands.AinSettings;
using TopDriveSystem.ConfigApp.AppControl.AinSettingsStorage;
using TopDriveSystem.ConfigApp.AppControl.CommandSenderHost;
using TopDriveSystem.ConfigApp.AppControl.TargetAddressHost;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsRead
{
    internal class AinSettingsReader : IAinSettingsReader, IAinSettingsReadNotifyRaisable
    {
        private readonly IAinSettingsStorageSettable _ainSettingsStorageSettable;
        private readonly ICommandSenderHost _commandSenderHost;
        private readonly ILogger _logger;
        private readonly IWorker<Action> _notifyWorker;
        private readonly TimeSpan _readSettingsTimeout;
        private readonly ITargetAddressHost _targerAddressHost;

        public AinSettingsReader(ICommandSenderHost commandSenderHost, ITargetAddressHost targerAddressHost,
            ILogger logger, IAinSettingsStorageSettable ainSettingsStorageSettable,
            IMultiLoggerWithStackTrace<int> debugLogger)
        {
            _commandSenderHost = commandSenderHost;
            _targerAddressHost = targerAddressHost;
            _logger = logger;
            _ainSettingsStorageSettable = ainSettingsStorageSettable;
            _readSettingsTimeout = TimeSpan.FromMilliseconds(200.0);
            _notifyWorker = new SingleThreadedRelayQueueWorker<Action>("AinSettingsReaderNotify", a => a(),
                ThreadPriority.BelowNormal, true, null, debugLogger.GetLogger(0));
        }

        public void ReadSettingsAsync(byte zeroBasedAinNumber, bool forceRead, Action<Exception, IAinSettings> callback)
        {
            if (forceRead == false)
            {
                // Берем настройки АИН из хранилища:
                var settings = _ainSettingsStorageSettable.GetSettings(zeroBasedAinNumber);
                if (settings != null)
                {
                    _notifyWorker.AddWork(() =>
                        callback.Invoke(null,
                            settings)); // TODO: callback fail unknown, use try -> invoke, catch -> log
                    return;
                }
            }

            // Вычитываем настройки из порта:
            var sender = _commandSenderHost.Sender;
            if (sender == null) throw new NullReferenceException("Порт передачи данных не открыт");

            var readSettingsCmd = new ReadAinSettingsCommand(zeroBasedAinNumber);
            _logger.Log("Чтение настроек АИН " + (zeroBasedAinNumber + 1) + "...");
            _notifyWorker.AddWork(() => FireEventAinSettingsReadStarted(zeroBasedAinNumber));
            sender.SendCommandAsync(_targerAddressHost.TargetAddress, readSettingsCmd, _readSettingsTimeout, 2,
                (sendException, replyBytes) =>
                {
                    if (sendException != null)
                    {
                        var errorMessage = "Произошла ошибка во время чтения настрок АИН" + (zeroBasedAinNumber + 1);
                        _logger.Log(errorMessage);
                        try
                        {
                            var ex = new Exception(errorMessage, sendException);

                            _notifyWorker.AddWork(() => callback.Invoke(ex, null));
                            _notifyWorker.AddWork(() => FireEventAinSettingsReadComplete(zeroBasedAinNumber, ex, null));
                            _notifyWorker.AddWork(() =>
                                _ainSettingsStorageSettable.SetSettings(zeroBasedAinNumber, null));
                        }
                        catch
                        {
                            _logger.Log(
                                "Не удалось совершить обратный вызов после неудачного чтения настроек (либо не удалось обnullить в хранилище) АИН" +
                                (zeroBasedAinNumber + 1));
                        }

                        return;
                    }

                    try
                    {
                        var result = readSettingsCmd.GetResult(replyBytes);
                        if (zeroBasedAinNumber == 0 && result.Ain1LinkFault)
                            throw new Exception("Настройки АИН1 были считаны, однако флаг ошибки связи с АИН1 взведен");
                        if (zeroBasedAinNumber == 1 && result.Ain2LinkFault)
                            throw new Exception("Настройки АИН2 были считаны, однако флаг ошибки связи с АИН2 взведен");
                        if (zeroBasedAinNumber == 2 && result.Ain3LinkFault)
                            throw new Exception("Настройки АИН3 были считаны, однако флаг ошибки связи с АИН3 взведен");

                        try
                        {
                            _notifyWorker.AddWork(() => callback.Invoke(null, result));
                            _notifyWorker.AddWork(() =>
                                FireEventAinSettingsReadComplete(zeroBasedAinNumber, null, result));
                            _notifyWorker.AddWork(() =>
                                _ainSettingsStorageSettable.SetSettings(zeroBasedAinNumber, result));
                            _logger.Log("Настройки АИН " + (zeroBasedAinNumber + 1) + " успешно прочитаны");
                        }
                        catch
                        {
                            _logger.Log(
                                "Не удалось совершить обратный вызов после успешного чтения настроек (либо не удалось сохранить настройки в хранилище) АИН" +
                                (zeroBasedAinNumber + 1));
                        }
                    }
                    catch (Exception resultGetException)
                    {
                        var errorMessage = "Ошибка во время разбора ответа на команду чтения настроек АИН" +
                                           (zeroBasedAinNumber + 1) + ": " + resultGetException.Message;
                        _logger.Log(errorMessage);
                        try
                        {
                            var ex = new Exception(errorMessage, resultGetException);
                            _notifyWorker.AddWork(() => callback.Invoke(ex, null));
                            _notifyWorker.AddWork(() => FireEventAinSettingsReadComplete(zeroBasedAinNumber, ex, null));
                            _notifyWorker.AddWork(() =>
                                _ainSettingsStorageSettable.SetSettings(zeroBasedAinNumber, null));
                        }
                        catch
                        {
                            _logger.Log(
                                "Не удалось совершить обратный вызов после неудачного парсинга настроек (либо не удалось обnullить в хранилище) АИН " +
                                (zeroBasedAinNumber + 1));
                        }
                    }
                });
        }

        public event AinSettingsReadCompleteDelegate AinSettingsReadComplete;
        public event AinSettingsReadStartedDelegate AinSettingsReadStarted;

        public void RaiseAinSettingsReadStarted(byte zbAinNumber)
        {
            _notifyWorker.AddWork(() => FireEventAinSettingsReadStarted(zbAinNumber));
        }

        public void RaiseAinSettingsReadComplete(byte zbAinNumber, Exception innerException, IAinSettings settings)
        {
            _notifyWorker.AddWork(() => FireEventAinSettingsReadComplete(zbAinNumber, innerException, settings));
        }

        private void FireEventAinSettingsReadComplete(byte zbAinNumber, Exception innerException, IAinSettings settings)
        {
            var eve = AinSettingsReadComplete;
            eve?.Invoke(zbAinNumber, innerException, settings);
        }

        private void FireEventAinSettingsReadStarted(byte zbAinNumber)
        {
            var eve = AinSettingsReadStarted;
            eve?.Invoke(zbAinNumber);
        }
    }
}