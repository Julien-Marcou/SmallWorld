using SmallWorld.Models.Tiles;
using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System.Collections.Generic;

namespace SmallWorld.Models.Interfaces
{
    interface IMap
    {
        int MaxPlayers { get; }

        int UnitPerPlayer { get; }

        int MaxTurns { get; }

        Dictionary<Point, Tile> Tiles { get; }

        Dictionary<Point, Player> Players { get; }

        Size Size { get; }

        int Seed { get; }

        bool TileIsControlled(Point tile);

        bool TileIsControlledBy(Point tile, Player player);

        Player GetTileController(Point tile);

        void PlayerControlsTile(Player player, Point tile);

        void PlayerReleasesTile(Player player, Point tile);

        bool HasTile(Point tile);

        Point GetTileUnder(Point tile);

        Point GetTileOver(Point tile);

        Point GetLeftTile(Point tile);

        Point GetRightTile(Point tile);

        Point GetTopLeftTile();

        bool IsReachableTile(Unit unit, Point origin, Point destination);

        bool AreAdjacent(Point origin, Point destination);

        List<Point> GetReachableTiles(Unit unit, Point origin);

        Point GetSpawn(int player);

        Dictionary<Point, Dictionary<Point, double>> GetRoadmap(Unit unit);
    }
}
