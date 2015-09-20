using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Factions
{
    [DataContract()]
    public class ElfFaction : Faction
    {
        public ElfFaction(int size, Point spawn) : base(size, spawn)
        {
        }

        protected override Unit BuildUnit()
        {
            return new ElfUnit();
        }
    }
}
