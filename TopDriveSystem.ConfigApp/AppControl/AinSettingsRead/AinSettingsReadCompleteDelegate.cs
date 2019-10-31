using System;
using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsRead
{
    internal delegate void AinSettingsReadCompleteDelegate(byte zeroBasedAinNumber, Exception readInnerException,
        IAinSettings settings);
}