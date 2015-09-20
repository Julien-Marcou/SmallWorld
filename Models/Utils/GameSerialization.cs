using SmallWorld.Models.AI;
using SmallWorld.Models.Factions;
using SmallWorld.Models.Maps;
using SmallWorld.Models.Tiles;
using SmallWorld.Models.Units;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Utils
{
    public class GameSerializer
    {
        public static DataContractSerializer Get()
        {
            return new DataContractSerializer(
                typeof(Game),
                new List<Type>
                {
                    typeof(ArtificialIntelligence),
                    typeof(DijkstraAI),
                    typeof(RandomAI),
                    typeof(DwarfFaction),
                    typeof(ElfFaction),
                    typeof(KnightFaction),
                    typeof(OrcFaction),
                    typeof(SlimeFaction),
                    typeof(Faction),
                    typeof(SmallMap),
                    typeof(NormalMap),
                    typeof(BigMap),
                    typeof(DeserticMap),
                    typeof(VegetalMap),
                    typeof(SwampMap),
                    typeof(CustomMap),
                    typeof(Map),
                    typeof(DesertTile),
                    typeof(ForestTile),
                    typeof(MountainTile),
                    typeof(PlainTile),
                    typeof(SeaTile),
                    typeof(SwampTile),
                    typeof(Tile),
                    typeof(DwarfUnit),
                    typeof(ElfUnit),
                    typeof(KnightUnit),
                    typeof(OrcUnit),
                    typeof(SlimeUnit),
                    typeof(Unit),
                    typeof(Color),
                    typeof(Point),
                    typeof(Size)
                }
            );
        }
    }
}
