using System;
using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ControlApp.Models.EngineSettingsSpace
{
    public delegate void EngineSettingsReadCompleteDelegate(Exception readInnerException, IEngineSettings settings);
}