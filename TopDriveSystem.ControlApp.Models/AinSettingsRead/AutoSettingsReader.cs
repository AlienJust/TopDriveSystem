﻿using AlienJust.Support.Loggers.Contracts;
using TopDriveSystem.ControlApp.Models.AinsCounter;
using TopDriveSystem.ControlApp.Models.AinSettingsStorage;
using TopDriveSystem.ControlApp.Models.EngineSettingsSpace;
using TopDriveSystem.ControlApp.Models.NotifySendingEnabled;

namespace TopDriveSystem.ControlApp.Models.AinSettingsRead
{
    public class AutoSettingsReader
    {
        private readonly IAinsCounter _ainsCounter;
        private readonly IAinSettingsReader _ainSettingsReader;
        private readonly IAinSettingsStorageSettable _ainSettingsStorageSettable;
        private readonly IEngineSettingsReader _engineSettingsReader;
        private readonly IEngineSettingsStorageSettable _engineSettingsStorageSettable;
        private readonly ILogger _logger;
        private readonly INotifySendingEnabled _sendingEnabledNotifier;

        public AutoSettingsReader(INotifySendingEnabled sendingEnabledNotifier,
            IAinsCounter ainsCounter, IAinSettingsReader ainSettingsReader,
            IAinSettingsStorageSettable ainSettingsStorageSettable,
            ILogger logger,
            IEngineSettingsReader engineSettingsReader, IEngineSettingsStorageSettable engineSettingsStorageSettable)
        {
            _ainsCounter = ainsCounter;
            _ainSettingsReader = ainSettingsReader;
            _ainSettingsStorageSettable = ainSettingsStorageSettable;
            _logger = logger;
            _engineSettingsReader = engineSettingsReader;
            _engineSettingsStorageSettable = engineSettingsStorageSettable;
            _sendingEnabledNotifier = sendingEnabledNotifier;

            _ainsCounter.AinsCountInSystemHasBeenChanged +=
                AinsCounterOnAinsCountInSystemHasBeenChanged; // TODO: unsubscribe
            _sendingEnabledNotifier.SendingEnabledChanged +=
                SendingEnabledNotifierOnSendingEnabledChanged; // TODO: unsubscribe on app quit
        }

        private void AinsCounterOnAinsCountInSystemHasBeenChanged(int ainsCounter)
        {
            if (_sendingEnabledNotifier.IsSendingEnabled) ReadSettings();
        }

        private void SendingEnabledNotifierOnSendingEnabledChanged(bool isSendingEnabled)
        {
            if (isSendingEnabled)
            {
                ReadSettings();
            }
            else
            {
                // independed of ains count in system, zeroing all ains settings in storage:
                _ainSettingsStorageSettable.SetSettings(0, null);
                _ainSettingsStorageSettable.SetSettings(1, null);
                _ainSettingsStorageSettable.SetSettings(2, null);

                _engineSettingsStorageSettable.SetSettings(null);
            }
        }

        private void ReadSettings()
        {
            // TODO: extract max ain nuber
            for (byte i = 0; i < 3; i++)
                if (i < _ainsCounter.SelectedAinsCount)
                {
                    _logger.Log("Автоматическое чтение настроек АИН №" + (i + 1) +
                                " при подключении к COM-порту / при изменении числа АИН");
                    _ainSettingsReader.ReadSettingsAsync(i, true,
                        (ex, settings) =>
                        {
                        }); // i dont need to know whether settings were readed or exception occured, just need to initiate read process
                }
                else
                {
                    _ainSettingsStorageSettable.SetSettings(i, null);
                }

            _engineSettingsReader.ReadSettingsAsync(true, (ex, settings) => { });
        }
    }
}