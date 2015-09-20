using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Factions
{
    [DataContract()]
    public class OrcFaction : Faction
    {
        public OrcFaction(int size, Point spawn) : base(size, spawn)
        {
        }

        protected override Unit BuildUnit()
        {
            return new OrcUnit();
        }
    }
}
