using SmallWorld.Models.Tiles;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Units
{
    [DataContract()]
    public class KnightUnit : Unit
    {
        public KnightUnit() : base()
        {
            AttackPoints = 2;
            DefensePoints = 2;
        }

        public override double GetMovementCost(Tile origin, Tile destination)
        {
            double baseCost = base.GetMovementCost(origin, destination);

            // The movement cost for knights on Plain tile is divided by 2
            if (destination.GetType() == typeof(PlainTile))
            {
                return baseCost / 2;
            }
            // The movement cost for knights on Swamp tile is multiplied by 2
            else if (destination.GetType() == typeof(SwampTile))
            {
                return baseCost * 2;
            }

            return baseCost;
        }
    }
}
