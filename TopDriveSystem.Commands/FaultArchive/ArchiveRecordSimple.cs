using System;
using AlienJust.Support.Numeric;

namespace TopDriveSystem.Commands.FaultArchive
{
    internal class ArchiveRecordSimple : IArchiveRecord
    {
        public byte Second { get; set; }
        public byte Minute { get; set; }
        public byte Hour { get; set; }
        public byte Day { get; set; }
        public byte Month { get; set; }
        public byte Year { get; set; }

        public DateTime Time => new DateTime(Year, Month, Day, Hour, Minute, Second);

        public ushort FaultState { get; set; }
        public ushort Mcw { get; set; }
        public ushort Msw { get; set; }
        public Crc16 Crc { get; set; }

        public bool IsCrcCorrect { get; set; }
    }
}