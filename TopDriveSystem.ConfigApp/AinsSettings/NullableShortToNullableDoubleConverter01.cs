using System;
using System.Globalization;
using System.Windows.Data;

namespace TopDriveSystem.ConfigApp.AinsSettings
{
    [ValueConversion(typeof(double), typeof(int))]
    internal class NullableShortToNullableDoubleConverter01 : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ns = (short?) value; // TODO: might throw exception?
            double? result;

            if (ns.HasValue)
                result = ns.Value * 0.1;
            else
                result = null;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var nd = (double?) value;

            short? result;

            if (nd.HasValue)
                result = (short) (nd.Value * 10.0);
            else
                result = null;

            return result;
        }

        #endregion
    }
}