using System.Runtime.Serialization;

namespace SmallWorld.Models.Utils
{
    [DataContract()]
    public class Color
    {
        [DataMember]
        public byte R { get; set; }

        [DataMember]
        public byte G { get; set; }

        [DataMember]
        public byte B { get; set; }

        [DataMember]
        public byte A { get; set; }

        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }

        public Color(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public override bool Equals(object obj)
        {
            var color = obj as Color;
            return color != null && R == color.R && G == color.G && B == color.B && A == color.A;
        }

        public override int GetHashCode()
        {
            var prime1 = 17;
            var prime2 = 23;

            int hash = prime1;
            hash = hash * prime2 + R.GetHashCode();
            hash = hash * prime2 + G.GetHashCode();
            hash = hash * prime2 + B.GetHashCode();
            hash = hash * prime2 + A.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return "Color: rgba(" + R + ", " + G + ", " + B + ", " + A + ")";
        }
    }
}
