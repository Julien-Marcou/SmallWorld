using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System;

namespace SmallWorld.Models.Factions
{
    public enum FactionType { Dwarf, Elf, Knight, Orc, Slime };

    public class FactionFactory
    {
        public static Faction GetFaction(FactionType type, int size, Point spawn)
        {
            switch(type)
            {
                case FactionType.Dwarf:
                    return new DwarfFaction(size, spawn);
                case FactionType.Elf:
                    return new ElfFaction(size, spawn);
                case FactionType.Knight:
                    return new KnightFaction(size, spawn);
                case FactionType.Orc:
                    return new OrcFaction(size, spawn);
                case FactionType.Slime:
                    return new SlimeFaction(size, spawn);
                default:
                    throw new ArgumentException("Impossible to create Faction of type \""+type+"\"", "type");
            }
        }

        public static FactionType GetType(Faction faction)
        {
            var type = faction.GetType().Name;
            switch (type)
            {
                case "DwarfFaction":
                    return FactionType.Dwarf;
                case "ElfFaction":
                    return FactionType.Elf;
                case "KnightFaction":
                    return FactionType.Knight;
                case "OrcFaction":
                    return FactionType.Orc;
                case "SlimeFaction":
                    return FactionType.Slime;
                default:
                    throw new ArgumentException("Impossible to retrieve type of Faction \"" + type + "\"", "faction");
            }
        }

        public static FactionType GetType(Unit unit)
        {
            var type = unit.GetType().Name;
            switch (type)
            {
                case "DwarfUnit":
                    return FactionType.Dwarf;
                case "ElfUnit":
                    return FactionType.Elf;
                case "KnightUnit":
                    return FactionType.Knight;
                case "OrcUnit":
                    return FactionType.Orc;
                case "SlimeUnit":
                    return FactionType.Slime;
                default:
                    throw new ArgumentException("Impossible to retrieve type of Unit \"" + type + "\"", "unit");
            }
        }
    }
}
