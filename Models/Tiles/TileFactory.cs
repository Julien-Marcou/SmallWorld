using System;
using System.Collections.Generic;

namespace SmallWorld.Models.Tiles
{
    public enum TileType { Desert, Forest, Mountain, Plain, Sea, Swamp };

    public class TileFactory
    {
        private static Random _randomizer = new Random();

        public static Random Randomizer
        {
            get { return _randomizer; }
            private set { _randomizer = value; }
        }

        public static void SetMapSeed(int seed)
        {
            Randomizer = new Random(seed);
        }

        public static Tile GetTile(TileType type)
        {
            switch (type)
            {
                case TileType.Desert:
                    return new DesertTile();
                case TileType.Forest:
                    return new ForestTile();
                case TileType.Mountain:
                    return new MountainTile();
                case TileType.Plain:
                    return new PlainTile();
                case TileType.Sea:
                    return new SeaTile();
                case TileType.Swamp:
                    return new SwampTile();
                default:
                    throw new ArgumentException("Impossible to create Tile of type \"" + type + "\"", "type");
            }
        }

        public static TileType GetType(Tile tile)
        {
            var type = tile.GetType().Name;
            switch (type)
            {
                case "DesertTile":
                    return TileType.Desert;
                case "ForestTile":
                    return TileType.Forest;
                case "MoutainTile":
                    return TileType.Mountain;
                case "PlainTile":
                    return TileType.Plain;
                case "SeaTile":
                    return TileType.Sea;
                case "SwampTile":
                    return TileType.Swamp;
                default:
                    throw new ArgumentException("Impossible to retrieve type of Tile \"" + type + "\"", "tile");
            }
        }

        public static List<Tile> GetRandomTiles(List<TileType> baseTiles, int count)
        {
            var tiles = new List<Tile>();
            for (var i = 0; i < count; i++)
            {
                tiles.Add(GetTile(baseTiles[i % baseTiles.Count]));
            }
            ShuffleTiles(tiles);
            return tiles;
        }

        private static void ShuffleTiles(List<Tile> tiles)
        {
            for (var currentIndex = 0; currentIndex < tiles.Count; currentIndex++)
            {
                var randomIndex = Randomizer.Next(tiles.Count);
                var tmpTile = tiles[currentIndex];
                tiles[currentIndex] = tiles[randomIndex];
                tiles[randomIndex] = tmpTile;
            }
        }
    }
}
