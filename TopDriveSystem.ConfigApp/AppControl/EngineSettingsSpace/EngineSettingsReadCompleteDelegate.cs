using System;
using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace
{
    delegate void EngineSettingsReadCompleteDelegate(Exception readInnerException, IEngineSettings settings);
}