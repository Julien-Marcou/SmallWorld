using SmallWorld.Models.Tiles;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Units
{
    [DataContract()]
    public class SlimeUnit : Unit
    {
        public SlimeUnit() : base()
        {
            MaxHealthPoints = HealthPoints = 3;
            AttackPoints = 1;
            DefensePoints = 2;
        }

        public override double GetMovementCost(Tile origin, Tile destination)
        {
            double baseCost = base.GetMovementCost(origin, destination);

            // The movement cost for slimes on Swamp tile is divided by 4
            if (destination.GetType() == typeof(SwampTile))
            {
                return baseCost / 4;
            }
            // The movement cost for slimes on Desert tile is multiplied by 2
            else if (destination.GetType() == typeof(DesertTile))
            {
                return baseCost * 2;
            }
            // The movement cost for slimes on Moutain tile is multiplied by 2
            else if (destination.GetType() == typeof(MountainTile))
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
            double chanceToRessurect = 0.2;
            if (randomizer.NextDouble() < chanceToRessurect)
            {
                HealthPoints = 1;
            }
        }
    }
}
