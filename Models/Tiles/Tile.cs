using SmallWorld.Models.Interfaces;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Tiles
{
    [DataContract()]
    public abstract class Tile : ITile
    {
        protected const int variantCount = 2;

        [DataMember]
        public int Variant { get; protected set; }

        public Tile()
        {
            Variant = TileFactory.Randomizer.Next(0, variantCount);
        }
    }
}
