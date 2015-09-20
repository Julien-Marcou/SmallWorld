using System.Windows;
using System.Windows.Media;

namespace SmallWorld.Styles
{
    public static class Hexagon
    {
        //public static readonly int OverlappingX = 0;
        public static readonly int OverlappingY = 1;
        public static readonly double Width = 136; // Must be divisible by 2
        public static readonly double Height = 156; // Must be divisible by 4
        public static readonly double HoverWidth = 130;
        public static readonly double HoverHeight = 150;
        public static readonly PointCollection Points = new PointCollection {
            new Point(68, 0),
            new Point(136, 38),
            new Point(136, 118),
            new Point(68, 156),
            new Point(0, 118),
            new Point(0, 38)
        };
    }
}
