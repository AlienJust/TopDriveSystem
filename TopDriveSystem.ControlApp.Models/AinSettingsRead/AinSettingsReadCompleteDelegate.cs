using System;
using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ControlApp.Models.AinSettingsRead
{
    public delegate void AinSettingsReadCompleteDelegate(byte zeroBasedAinNumber, Exception readInnerException,
        IAinSettings settings);
}