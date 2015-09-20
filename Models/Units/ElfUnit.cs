using SmallWorld.Models.Tiles;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Units
{
    [DataContract()]
    public class ElfUnit : Unit
    {
        public override double GetMovementCost(Tile origin, Tile destination)
        {
            double baseCost = base.GetMovementCost(origin, destination);

            // The movement cost for elfs on Forest tile is divided by 2
            if (destination.GetType() == typeof(ForestTile))
            {
                return baseCost / 2;
            }
            // The movement cost for elfs on Desert tile is multiplied by 2
            else if (destination.GetType() == typeof(DesertTile))
            {
                return baseCost * 2;
            }

            return baseCost;
        }

        public override void BeforeDying()
        {
            // Call parent method
            base.BeforeDying();

            // Trying to resurrect it (if succeeds, let it with 1 healthPoints)
            double chanceToRessurect = 0.5;
            if (randomizer.NextDouble() < chanceToRessurect)
            {
                HealthPoints = 1;
            }
        }
    }
}
