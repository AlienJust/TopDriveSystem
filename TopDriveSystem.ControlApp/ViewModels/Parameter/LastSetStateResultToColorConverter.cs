using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace TopDriveSystem.ControlApp.ViewModels.Parameter
{
    public sealed class LastSetStateResultToColorConverter : IValueConverter
    {
        public Color UnknownColor { get; set; }
        public Color UnsuccessColor { get; set; }
        public Color SuccessColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var lastValue = (LastSetStateResult) value;
            return lastValue switch
            {
                LastSetStateResult.Unknown => UnknownColor,
                LastSetStateResult.Success => SuccessColor,
                LastSetStateResult.Unsuccess => UnsuccessColor,
                _ => throw new ArgumentOutOfRangeException(nameof(value)),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var lastValue = (Color) value;
            if (lastValue.Equals(UnknownColor))
                return LastSetStateResult.Unknown;
            if (lastValue.Equals(SuccessColor))
                return LastSetStateResult.Success;
            if (lastValue.Equals(UnsuccessColor))
                return LastSetStateResult.Unsuccess;

            throw new ArgumentOutOfRangeException(nameof(value));
        }
    }
}