using SmallWorld.Models.Interfaces;
using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Factions
{
    [DataContract()]
    public abstract class Faction : IFaction
    {
        [DataMember]
        public Point Spawn { get; protected set; }

        [DataMember]
        public int Size { get; protected set; }

        [DataMember]
        public Dictionary<Point, List<Unit>> Units { get; protected set; }

        public int Score
        {
            get
            {
                int score = 0;
                foreach (var tileUnits in Units)
                {
                    foreach (var unit in tileUnits.Value)
                    {
                        score += unit.Score;
                    }
                }
                return score;
            }
        }

        public Faction(int size, Point spawn)
        {
            Size = size;
            Spawn = spawn;
            BuildUnits();
        }

        protected virtual void BuildUnits()
        {
            Units = new Dictionary<Point, List<Unit>>();
            var unitsOnSpawn = new List<Unit>();
            for (int i = 0; i < Size; i++)
            {
                unitsOnSpawn.Add(BuildUnit());
            }
            Units.Add(Spawn, unitsOnSpawn);
        }

        protected abstract Unit BuildUnit();

        public int UnitCount()
        {
            int unitCount = 0;
            foreach (var unitsOnTile in Units)
            {
                unitCount += unitsOnTile.Value.Count;
            }
            return unitCount;
        }

        public int UnitCountOn(Point tile)
        {
            if (Units.ContainsKey(tile))
            {
                return Units[tile].Count;
            }
            return 0;
        }

        public bool HasUnits()
        {
            return UnitCount() > 0;
        }

        public bool HasUnitsOn(Point tile)
        {
            return UnitCountOn(tile) > 0;
        }

        public Unit GetFirstUnitOn(Point tile)
        {
            if (Units.ContainsKey(tile) && Units[tile].Count > 0)
            {
                return Units[tile][0];
            }
            return null;
        }

        public Unit GetBestUnitOn(Point tile)
        {
            Unit bestUnit = null;
            if (Units.ContainsKey(tile) && Units[tile].Count > 0)
            {
                foreach (var unit in Units[tile])
                {
                    if (bestUnit == null || bestUnit.DefensePoints < unit.DefensePoints)
                    {
                        bestUnit = unit;
                    }
                }
            }
            return bestUnit;
        }

        public void AddUnit(Unit unit, Point tile)
        {
            if (!Units.ContainsKey(tile))
            {
                Units[tile] = new List<Unit>();
            }
            Units[tile].Add(unit);
        }

        public bool RemoveUnit(Unit unit, Point tile)
        {
            if (Units.ContainsKey(tile) && Units[tile].Contains(unit))
            {
                Units[tile].Remove(unit);
                if (Units[tile].Count == 0)
                {
                    Units.Remove(tile);
                }
                return true;
            }
            return false;
        }

        public void MoveUnit(Unit unit, Point origin, Point destination)
        {
            if (RemoveUnit(unit, origin))
            {
                AddUnit(unit, destination);
            }
        }

        public void ResetMovementPoints()
        {
            foreach (var tileUnits in Units)
            {
                foreach (var unit in tileUnits.Value)
                {
                    unit.ResetMovementPoints();
                }
            }
        }
    }
}
