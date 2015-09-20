using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System.Collections.Generic;

namespace SmallWorld.Models.Interfaces
{
    interface IFaction
    {
        Point Spawn { get; }

        int Size { get; }

        Dictionary<Point, List<Unit>> Units { get; }

        int Score { get; }

        int UnitCount();

        bool HasUnits();

        int UnitCountOn(Point tile);

        bool HasUnitsOn(Point tile);

        Unit GetFirstUnitOn(Point tile);

        Unit GetBestUnitOn(Point tile);

        void AddUnit(Unit unit, Point tile);

        bool RemoveUnit(Unit unit, Point tile);

        void MoveUnit(Unit unit, Point origin, Point destination);

        void ResetMovementPoints();
    }
}
