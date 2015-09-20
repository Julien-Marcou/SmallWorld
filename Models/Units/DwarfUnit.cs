using SmallWorld.Models.Tiles;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Units
{
    [DataContract()]
    public class DwarfUnit : Unit
    {
        public override bool CanMove(Tile origin, Tile destination, bool areAdjacent)
        {
            // A Dwarf can move like any other Unit,
            // But can also moves (teleports) if the origin and destination Tile are MoutainTile
            return (
                base.CanMove(origin, destination, areAdjacent)
                ||
                (
                    MovementPoints >= GetMovementCost(origin, destination)
                    &&
                    origin.GetType() == typeof(MountainTile)
                    &&
                    destination.GetType() == typeof(MountainTile)
                )
            );
        }
    }
}
