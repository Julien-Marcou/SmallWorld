using SmallWorld.Models.AI;
using SmallWorld.Models.Factions;
using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System.Collections.Generic;

namespace SmallWorld.Models.Interfaces
{
    interface IPlayer
    {
        Point Spawn { get; }

        string Name { get; }

        Color Color { get; }

        Faction Faction { get; }

        int Score { get; }

        ArtificialIntelligence AI { get; }

        bool IsAI();

        int UnitCount();

        int UnitCountOn(Point tile);

        bool HasUnits();

        bool HasUnitsOn(Point tile);

        Dictionary<Point, List<Unit>> GetUnits();

        Unit GetFirstUnitOn(Point tile);

        Unit GetBestUnitOn(Point tile);

        void AddUnit(Unit unit, Point tile);

        bool RemoveUnit(Unit unit, Point tile);

        void MoveUnit(Unit unit, Point origin, Point destination);

        void ResetMovementPoints();
    }
}
