using System;
using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsRead {
	delegate void AinSettingsReadCompleteDelegate(byte zeroBasedAinNumber, Exception readInnerException, IAinSettings settings);
}