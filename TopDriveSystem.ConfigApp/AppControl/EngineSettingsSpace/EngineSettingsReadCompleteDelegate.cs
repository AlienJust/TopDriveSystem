using System;
using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace
{
    internal delegate void EngineSettingsReadCompleteDelegate(Exception readInnerException, IEngineSettings settings);
}