using SmallWorld.Models.Utils;
using System;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Maps
{
    [DataContract()]
    public class BigMap : Map
    {
        public override int MaxPlayers { get { return 2; } }

        public override int UnitPerPlayer { get { return 8; } }

        public override int MaxTurns { get { return 20; } }
        
        public BigMap(int seed) : base(new Size(7, 6), seed)
        {
        }
    }
}
