using System;

namespace TopDriveSystem.Commands.AinTelemetry
{
    internal static class DateTimeExtensions
    {
        public static DateTime? FromUshort(this ushort value)
        {
            DateTime? buildDate;
            try
            {
                var year = 2000 + ((value >> 9) & 0x7F);
                var month = (value >> 5) & 0x0F;
                var day = value & 0x1F;
                buildDate = new DateTime(year, month, day);
            }
            catch
            {
                buildDate = null;
            }

            return buildDate;
        }
    }
}