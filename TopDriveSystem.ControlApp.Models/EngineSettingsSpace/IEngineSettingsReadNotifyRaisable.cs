using System;
using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ControlApp.Models.EngineSettingsSpace
{
    public interface IEngineSettingsReadNotifyRaisable : IEngineSettingsReadNotify
    {
        void RaiseEngineSettingsReadStarted();
        void RaiseEngineSettingsReadComplete(Exception innerException, IEngineSettings settings);
    }
}