using SmallWorld.Models.Tiles;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Units
{
    [DataContract()]
    public class OrcUnit : Unit
    {
        public override double GetMovementCost(Tile origin, Tile destination)
        {
            double baseCost = base.GetMovementCost(origin, destination);

            // The movement cost for orcs on Plain tile is divided by 2
            if (destination.GetType() == typeof(PlainTile))
            {
                return baseCost / 2;
            }

            return baseCost;
        }

        public override void AfterKill()
        {
            // When an orc kill another unit, he gains 1 score point
            Score += 1;
        }
    }
}
