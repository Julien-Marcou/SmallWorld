using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SmallWorld.Converters
{
    class StringToStyle : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                throw new ArgumentException("StringToStyle converter needs the value to not be null !", "value");
            }
            if(parameter != null)
            {
                value += (string)parameter;
            }
            return Application.Current.FindResource(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("StringToStyle is a One-Way converter only !");
        }
    }
}
