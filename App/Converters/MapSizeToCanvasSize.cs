using SmallWorld.Models.Utils;
using SmallWorld.Styles;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SmallWorld.Converters
{
    class MapSizeToCanvasSize : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter", "MapSizeToCanvasSize converter needs the parameter !");
            }

            string dimension = (string)parameter;

            if (dimension != "Width" && dimension != "Height")
            {
                throw new ArgumentException("MapSizeToCanvasSize parameter needs to be \"Width\" or \"Height\" !", "parameter");
            }

            var size = (Size)value;

            if (dimension == "Width")
            {
                return (size.Width * Hexagon.Width) + Hexagon.Points[0].X;
            }
            return ((size.Height - 1) * (Hexagon.Points[2].Y - Hexagon.OverlappingY)) + Hexagon.Height;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("MapSizeToCanvasSize is a One-Way converter only !");
        }
    }
}

