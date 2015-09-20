using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SmallWorld.Converters
{
    internal class BooleanToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isTrue = Visibility.Visible;
            var isFalse = Visibility.Collapsed;
            if ((string)parameter == "ReverseVisibility")
            {
                var tmp = isTrue;
                isTrue = isFalse;
                isFalse = tmp;
            }

            if ((bool)value)
            {
                return isTrue;
            }
            return isFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("BooleanToVisibility is a One-Way converter only !");
        }
    }
}