using System.Runtime.Serialization;

namespace SmallWorld.Models.Utils
{
    [DataContract()]
    public class Point
    {
        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            var point = obj as Point;
            return point != null && X == point.X && Y == point.Y;
        }

        public override int GetHashCode()
        {
            var prime1 = 17;
            var prime2 = 23;

            int hash = prime1;
            hash = hash * prime2 + X.GetHashCode();
            hash = hash * prime2 + Y.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return "Point: " + X + ", " + Y;
        }
    }
}
