using SmallWorld.Models.Interfaces;
using SmallWorld.Models.Tiles;
using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Maps
{
    [DataContract()]
    public abstract class Map : IMap
    {
        public virtual int MaxPlayers { get { return 2; } }
        
        public virtual int UnitPerPlayer { get { return 4; } }
        
        public virtual int MaxTurns { get { return 14; } }

        // Storage using Axial Coordinates on Pointy Topped Hexagons
        // See: http://www.redblobgames.com/grids/hexagons/#map-storage
        [DataMember]
        public Dictionary<Point, Tile> Tiles { get; protected set; }

        [DataMember]
        public Dictionary<Point, Player> Players { get; protected set; }

        [DataMember]
        public Size Size { get; protected set; }

        [DataMember]
        public int Seed { get; protected set; }

        public Map(Size size, int seed) : this(size, seed, new List<TileType> { TileType.Desert, TileType.Forest, TileType.Mountain, TileType.Plain })
        {
        }

        public Map(Size size, int seed, List<TileType> baseTiles)
        {
            Size = size;
            Seed = seed;
            Tiles = new Dictionary<Point, Tile>();
            Players = new Dictionary<Point, Player>();

            TileFactory.SetMapSeed(Seed);
            
            BuildTiles(baseTiles);
        }

        protected virtual void BuildTiles(List<TileType> baseTiles)
        {
            // Builds a random rectangular Map
            var randomTiles = TileFactory.GetRandomTiles(baseTiles, Size.Width * Size.Height);
            var i = 0;
            for (var y = 0; y < Size.Height; y++)
            {
                for (var x = 0; x < Size.Width; x++)
                {
                    var point = OffsetToAxial(new Point(x, y));
                    var tile = randomTiles[i++];
                    Tiles.Add(point, tile);
                }
            }
        }

        public bool TileIsControlled(Point tile)
        {
            return Players.ContainsKey(tile);
        }

        public bool TileIsControlledBy(Point tile, Player player)
        {
            return GetTileController(tile) == player;
        }

        public Player GetTileController(Point tile)
        {
            if (TileIsControlled(tile))
            {
                return Players[tile];
            }
            return null;
        }

        public void PlayerControlsTile(Player player, Point tile)
        {
            if (Players.ContainsKey(tile))
            {
                Players[tile] = player;
            }
            else
            {
                Players.Add(tile, player);
            }
        }

        public void PlayerReleasesTile(Player player, Point tile)
        {
            if (Players.ContainsKey(tile))
            {
                Players.Remove(tile);
            }
        }

        public bool HasTile(Point tile)
        {
            return Tiles.ContainsKey(tile);
        }

        public Point GetTileUnder(Point tile)
        {
            int x = (int)tile.X;
            int y = (int)tile.Y;
            if (tile.Y % 2 == 1)
            {
                x--;
            }
            return new Point(x, y + 1);
        }

        public Point GetTileOver(Point tile)
        {
            int x = (int)tile.X;
            int y = (int)tile.Y;
            if (tile.Y % 2 == 0)
            {
                x++;
            }
            return new Point(x, y - 1);
        }

        public Point GetLeftTile(Point tile)
        {
            int x = (int)tile.X;
            int y = (int)tile.Y;
            return new Point(x - 1, y);
        }

        public Point GetRightTile(Point tile)
        {
            int x = (int)tile.X;
            int y = (int)tile.Y;
            return new Point(x + 1, y);
        }

        public virtual Point GetTopLeftTile()
        {
            return new Point(0, 0);
        }

        public bool IsReachableTile(Unit unit, Point origin, Point destination)
        {
            return unit.CanMove(Tiles[origin], Tiles[destination], AreAdjacent(origin, destination));
        }

        public bool AreAdjacent(Point origin, Point destination)
        {
            int xDistance = origin.X - destination.X;
            int yDistance = origin.Y - destination.Y;
            return (
                xDistance == 1 && yDistance == 0
                ||
                xDistance == -1 && yDistance == 0
                ||
                xDistance == 0 && yDistance == 1
                ||
                xDistance == 0 && yDistance == -1
                ||
                xDistance == -1 && yDistance == 1
                ||
                xDistance == 1 && yDistance == -1
            );
        }

        public List<Point> GetReachableTiles(Unit unit, Point origin)
        {
            var reachableTiles = new List<Point>();
            foreach (var tile in Tiles)
            {
                var destination = tile.Key;
                if (IsReachableTile(unit, origin, destination))
                {
                    reachableTiles.Add(destination);
                }
            }
            return reachableTiles;
        }

        public virtual Point GetSpawn(int player)
        {
            if (player == 0)
            {
                return Tiles.First().Key;
            }
            else if (player == 1)
            {
                return Tiles.Last().Key;
            }

            throw new ArgumentException("Current Map implementation only supports 2 players", "player");
        }

        public Dictionary<Point, Dictionary<Point, double>> GetRoadmap(Unit unit)
        {
            var roadmap = new Dictionary<Point, Dictionary<Point, double>>();
            foreach (var tile in Tiles)
            {
                var origin = tile.Key;
                var reachableTiles = GetReachableTiles(unit, origin);
                roadmap.Add(origin, new Dictionary<Point, double>());

                foreach(var destination in reachableTiles)
                {
                    var cost = unit.GetMovementCost(Tiles[origin], Tiles[destination]);
                    roadmap[origin].Add(destination, cost);
                }
            }
            return roadmap;
        }

        private static Point AxialToOffset(Point coordinates)
        {
            // From Axial Coordinates
            // To "odd-r" Offset Coordinates
            // On Pointy Topped Hexagons
            // See: http://www.redblobgames.com/grids/hexagons/#conversions
            int x = coordinates.X + (coordinates.Y - (coordinates.Y & 1)) / 2;
            int y = coordinates.Y;
            return new Point(x, y);
        }

        private static Point OffsetToAxial(Point coordinates)
        {
            // From "odd-r" Offset Coordinates
            // To Axial Coordinates
            // On Pointy Topped Hexagons
            // See: http://www.redblobgames.com/grids/hexagons/#conversions
            int x = coordinates.X - (coordinates.Y - (coordinates.Y & 1)) / 2;
            int y = coordinates.Y;
            return new Point(x, y);
        }

        private static Tuple<int, int, int> AxialToCube(Point coordinates)
        {
            // From Axial Coordinates
            // To Cube Coordinates
            // On Pointy Topped Hexagons
            // See: http://www.redblobgames.com/grids/hexagons/#conversions
            int x = coordinates.X;
            int y = -coordinates.X - coordinates.Y;
            int z = coordinates.Y;
            return new Tuple<int, int, int>(x, y, z);
        }

        private static Point CubeToAxial(Tuple<int, int, int> coordinates)
        {
            // From Cube Coordinates
            // To Axial Coordinates
            // On Pointy Topped Hexagons
            // See: http://www.redblobgames.com/grids/hexagons/#conversions
            int x = coordinates.Item1;
            int y = coordinates.Item3;
            return new Point(x, y);
        }

        private static int Distance(Point origin, Point destination)
        {
            // Using Axial Coordinates (and converting them to Cube Coordinates)
            // See: http://www.redblobgames.com/grids/hexagons/#distances
            var cubeOrigin = AxialToCube(origin);
            var xOrigin = cubeOrigin.Item1;
            var yOrigin = cubeOrigin.Item2;
            var zOrigin = cubeOrigin.Item3;

            var cubeDestination = AxialToCube(destination);
            var xDestination = cubeOrigin.Item1;
            var yDestination = cubeOrigin.Item2;
            var zDestination = cubeOrigin.Item3;

            return Math.Max(Math.Abs(xDestination - xOrigin), Math.Max(Math.Abs(yDestination - yOrigin), Math.Abs(zDestination - zOrigin)));
        }
    }
}
