using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Factions
{
    [DataContract()]
    public class SlimeFaction : Faction
    {
        public SlimeFaction(int size, Point spawn) : base((int)Math.Round(size * 1.5, 0, MidpointRounding.AwayFromZero), spawn)
        {
            // Slime Faction start with 50% more units
        }

        protected override Unit BuildUnit()
        {
            return new SlimeUnit();
        }
    }
}
