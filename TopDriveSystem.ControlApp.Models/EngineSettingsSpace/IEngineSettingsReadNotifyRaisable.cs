using System;
using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace
{
    public interface IEngineSettingsReadNotifyRaisable : IEngineSettingsReadNotify
    {
        void RaiseEngineSettingsReadStarted();
        void RaiseEngineSettingsReadComplete(Exception innerException, IEngineSettings settings);
    }
}