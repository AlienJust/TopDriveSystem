using System;
using TopDriveSystem.Commands.AinSettings;
using TopDriveSystem.ControlApp.Models.AinSettingsRead;
using TopDriveSystem.ControlApp.Models.AinSettingsWrite;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.AinSettingsRw
{
    internal class AinSettingsReaderWriter : IAinSettingsReaderWriter
    {
        private readonly IAinSettingsReader _reader;
        private readonly IAinSettingsWriter _writer;

        public AinSettingsReaderWriter(IAinSettingsReader reader, IAinSettingsWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public void ReadSettingsAsync(byte zeroBasedAinNumber, bool forceRead, Action<Exception, IAinSettings> callback)
        {
            _reader.ReadSettingsAsync(zeroBasedAinNumber, forceRead, callback);
        }

        public void WriteSettingsAsync(IAinSettingsPart settings, Action<Exception> callback)
        {
            _writer.WriteSettingsAsync(settings, callback);
        }
    }
}