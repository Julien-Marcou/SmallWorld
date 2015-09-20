using SmallWorld.Models.Utils;
using SmallWorld.Styles;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SmallWorld.Converters
{
    class AxialToPixel : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter", "AxialToPixel converter needs the parameter !");
            }

            string axis = (string)parameter;

            if (axis != "X" && axis != "Y")
            {
                throw new ArgumentException("AxialToPixel parameter needs to be \"X\" or \"Y\" !", "parameter");
            }

            var coordinates = (Point)value;

            // From Axial Coordinates
            // To Pixel Coordinates
            // On Pointy Topped Hexagons
            // See: http://www.redblobgames.com/grids/hexagons/#hex-to-pixel

            // V1
            //var size = Hexagon.Height / 2.0;
            //var x = size * Math.Sqrt(3.0) * (coordinates.X + coordinates.Y / 2.0);
            //var y = size * 3.0 / 2.0 * coordinates.Y;
            //var point = new Point((int)Math.Round(x), (int)Math.Round(y));

            // V2 (almost the same as V1)
            //var x = (coordinates.X * Hexagon.Width) + (coordinates.Y * Hexagon.Width / 2.0);
            //var y = coordinates.Y * Hexagon.Height * 3.0 / 4.0;
            //var point = new Point((int)Math.Round(x), (int)Math.Round(y));

            //// V3 (pixel perfect)
            var x = (coordinates.X * Hexagon.Points[2].X) + (coordinates.Y * Hexagon.Points[0].X);
            var y = (coordinates.Y * (Hexagon.Points[2].Y - Hexagon.OverlappingY));
            var point = new Point((int)x, (int)y);

            if (axis == "X")
            {
                return point.X;
            }
            return point.Y;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("AxialToPixel is a One-Way converter only !");
        }
    }
}
