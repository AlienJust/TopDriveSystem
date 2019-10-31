using System;
using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ControlApp.Models.EngineSettingsSpace
{
    internal interface IAinSettingsReadNotifyRaisable : IEngineSettingsReadNotify
    {
        void RaiseEngineSettingsReadStarted();
        void RaiseEngineSettingsReadComplete(Exception innerException, IEngineSettings settings);
    }
}