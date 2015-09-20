using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace SmallWorld.Converters
{
    class ItemsEqualToVisibility : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Count() != 2)
            {
                throw new ArgumentException("ItemsEqualToVisibility converter needs 2 values [item1, item2] !", "values");
            }

            var equal = Visibility.Visible;
            var notEqual = Visibility.Collapsed;
            if ((string)parameter == "ReverseVisibility")
            {
                var tmp = equal;
                equal = notEqual;
                notEqual = tmp;
            }

            if (values[0] == null || values[1] == null)
            {
                if (values[0] == null && values[1] == null)
                {
                    return equal;
                }
                return notEqual;
            }
            if (values[0].Equals(values[1]))
            {
                return equal;
            }
            return notEqual;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("ItemsEqualToVisibility is a One-Way converter only !");
        }
    }
}
