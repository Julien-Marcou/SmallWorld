using SmallWorld.Models.AI;
using SmallWorld.Models.Factions;
using SmallWorld.Models.Interfaces;
using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmallWorld.Models
{
    [DataContract(IsReference = true)]
    public class Player : IPlayer
    {
        [DataMember]
        public Point Spawn { get; protected set; }

        [DataMember]
        public string Name { get; protected set; }

        [DataMember]
        public Color Color { get; protected set; }

        [DataMember]
        public Faction Faction { get; protected set; }

        public int Score
        {
            get
            {
                return Faction.Score;
            }
        }

        [DataMember]
        public ArtificialIntelligence AI { get; protected set; }

        public Player(string name, Color color, Point spawn, Faction faction)
        {
            Name = name;
            Color = color;
            Spawn = spawn;
            Faction = faction;
            AI = null;
        }

        public void SetAI(ArtificialIntelligence ai)
        {
            AI = ai;
        }

        public bool IsAI()
        {
            return AI != null;
        }

        public int UnitCount()
        {
            return Faction.UnitCount();
        }

        public int UnitCountOn(Point tile)
        {
            return Faction.UnitCountOn(tile);
        }

        public bool HasUnits()
        {
            return Faction.HasUnits();
        }

        public bool HasUnitsOn(Point tile)
        {
            return Faction.HasUnitsOn(tile);
        }

        public Dictionary<Point, List<Unit>> GetUnits()
        {
            return Faction.Units;
        }

        public Unit GetFirstUnitOn(Point tile)
        {
            return Faction.GetFirstUnitOn(tile);
        }

        public Unit GetBestUnitOn(Point tile)
        {
            return Faction.GetBestUnitOn(tile);
        }

        public void AddUnit(Unit unit, Point tile)
        {
            Faction.AddUnit(unit, tile);
        }

        public bool RemoveUnit(Unit unit, Point tile)
        {
            return Faction.RemoveUnit(unit, tile);
        }

        public void MoveUnit(Unit unit, Point origin, Point destination)
        {
            Faction.MoveUnit(unit, origin, destination);
        }

        public void ResetMovementPoints()
        {
            Faction.ResetMovementPoints();
        }
    }
}
