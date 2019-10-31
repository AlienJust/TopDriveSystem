using System;
using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsRead
{
    internal interface IAinSettingsReadNotifyRaisable : IAinSettingsReadNotify
    {
        void RaiseAinSettingsReadStarted(byte zbAinNumber);
        void RaiseAinSettingsReadComplete(byte zbAinNumber, Exception innerException, IAinSettings settings);
    }
}