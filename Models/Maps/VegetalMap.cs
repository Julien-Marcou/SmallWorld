using SmallWorld.Models.Tiles;
using SmallWorld.Models.Utils;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Maps
{
    [DataContract()]
    public class VegetalMap : Map
    {
        public VegetalMap(int seed) : base(new Size(5, 4), seed, new List<TileType> { TileType.Forest, TileType.Plain })
        {
        }
    }
}
