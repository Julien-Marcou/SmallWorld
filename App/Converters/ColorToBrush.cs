using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SmallWorld.Converters
{
    class ColorToBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Models.Utils.Color)value;
            return new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("ColorToBrush is a One-Way converter only !");
        }
    }
}
