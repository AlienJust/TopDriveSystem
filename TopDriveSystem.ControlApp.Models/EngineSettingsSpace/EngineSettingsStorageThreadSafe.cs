using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ControlApp.Models.EngineSettingsSpace
{
    internal class EngineSettingsStorageThreadSafe : IEngineSettingsStorageSettable, IEngineSettingsStorageUpdatedNotify
    {
        private readonly object _engineSettingsSync;
        private IEngineSettings _engineSettings;

        public EngineSettingsStorageThreadSafe()
        {
            _engineSettingsSync = new object();
            _engineSettings = null;
        }

        public IEngineSettings EngineSettings
        {
            get
            {
                lock (_engineSettingsSync)
                {
                    return _engineSettings;
                }
            }
        }

        public void SetSettings(IEngineSettings settings)
        {
            lock (_engineSettingsSync)
            {
                _engineSettings = settings;
            }

            RaiseAinSettingsUpdated(settings);
        }

        public event StoredEngineSettingsUpdatedDelegate EngineSettingsUpdated;

        private void RaiseAinSettingsUpdated(IEngineSettings settings)
        {
            var eve = EngineSettingsUpdated;
            eve?.Invoke(settings);
        }
    }
}