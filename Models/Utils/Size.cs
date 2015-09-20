using System.Runtime.Serialization;

namespace SmallWorld.Models.Utils
{
    [DataContract()]
    public class Size
    {
        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override bool Equals(object obj)
        {
            var size = obj as Size;
            return size != null && Width == size.Width && Height == size.Height;
        }

        public override int GetHashCode()
        {
            var prime1 = 17;
            var prime2 = 23;

            int hash = prime1;
            hash = hash * prime2 + Width.GetHashCode();
            hash = hash * prime2 + Height.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return "Size: " + Width + "x" + Height;
        }
    }
}
