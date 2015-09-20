using SmallWorld.Models.Utils;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Maps
{
    [DataContract()]
    public class SmallMap : Map
    {
        public override int MaxPlayers { get { return 2; } }

        public override int UnitPerPlayer { get { return 3; } }

        public override int MaxTurns { get { return 10; } }
        
        public SmallMap(int seed) : base(new Size(3, 3), seed)
        {
        }
    }
}
