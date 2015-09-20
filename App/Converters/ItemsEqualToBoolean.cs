using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SmallWorld.Converters
{
    class ItemsEqualToBoolean : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Count() != 2)
            {
                throw new ArgumentException("ItemsEqualToBoolean converter needs 2 values [item1, item2] !", "values");
            }

            var equal = true;
            var notEqual = false;
            if ((string)parameter == "ReverseBoolean")
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
            throw new InvalidOperationException("ItemsEqualToBoolean is a One-Way converter only !");
        }
    }
}
