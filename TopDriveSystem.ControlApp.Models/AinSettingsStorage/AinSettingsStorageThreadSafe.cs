﻿using System.Collections.Generic;
using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsStorage
{
    internal class AinSettingsStorageThreadSafe : IAinSettingsStorageSettable, IAinSettingsStorageUpdatedNotify
    {
        private readonly object _ainSettingsSync;
        private readonly List<IAinSettings> _ainsSettings;

        public AinSettingsStorageThreadSafe()
        {
            _ainSettingsSync = new object();
            _ainsSettings = new List<IAinSettings> {null, null, null};
        }

        public IAinSettings GetSettings(byte zeroBasedAinNumber)
        {
            lock (_ainSettingsSync)
            {
                return _ainsSettings[zeroBasedAinNumber];
            }
        }

        public void SetSettings(byte zeroBasedAinNumber, IAinSettings settings)
        {
            lock (_ainSettingsSync)
            {
                _ainsSettings[zeroBasedAinNumber] = settings;
            }

            RaiseAinSettingsUpdated(zeroBasedAinNumber, settings);
        }

        public event StoredAinSettingsUpdatedDelegate AinSettingsUpdated;

        private void RaiseAinSettingsUpdated(byte zeroBasedAinNumber, IAinSettings settings)
        {
            var eve = AinSettingsUpdated;
            eve?.Invoke(zeroBasedAinNumber, settings);
        }
    }
}