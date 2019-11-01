using System;
using System.Globalization;
using System.Windows.Data;

namespace TopDriveSystem.ConfigApp.AinCommand
{
    internal class NullableBoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool nb)) return " ? ";
            return nb ? " ☒ " : " ☐ ";
            //
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}