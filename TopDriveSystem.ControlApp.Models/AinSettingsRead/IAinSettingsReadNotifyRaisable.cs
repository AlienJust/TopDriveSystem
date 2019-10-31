using System;
using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ControlApp.Models.AinSettingsRead
{
    public interface IAinSettingsReadNotifyRaisable : IAinSettingsReadNotify
    {
        void RaiseAinSettingsReadStarted(byte zbAinNumber);
        void RaiseAinSettingsReadComplete(byte zbAinNumber, Exception innerException, IAinSettings settings);
    }
}