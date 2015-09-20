using SmallWorld.Models.Tiles;
using SmallWorld.Models.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Maps
{
    [DataContract()]
    public class CustomMap : Map
    {
        public override int MaxPlayers { get { return 2; } }

        public override int UnitPerPlayer { get { return 5; } }

        public override int MaxTurns { get { return 16; } }

        public CustomMap(int seed) : base(new Size(8, 6), seed)
        {
        }

        public override Point GetSpawn(int player)
        {
            if (player == 0)
            {
                return new Point(5, 0);
            }
            else if (player == 1)
            {
                return new Point(0, 5);
            }

            throw new ArgumentException("Current CustomMap implementation only supports 2 players", "player");
        }

        public override Point GetTopLeftTile()
        {
            return new Point(0, 0);
        }

        protected override void BuildTiles(List<TileType> baseTiles)
        {
            Tiles[new Point(0, 0)] = TileFactory.GetTile(TileType.Desert);
            //Tiles[new Point(0, 1)] = null;
            //Tiles[new Point(-1, 2)] = null;
            Tiles[new Point(-1, 3)] = TileFactory.GetTile(TileType.Forest);
            Tiles[new Point(-2, 4)] = TileFactory.GetTile(TileType.Plain);
            Tiles[new Point(-2, 5)] = TileFactory.GetTile(TileType.Plain);

            Tiles[new Point(1, 0)] = TileFactory.GetTile(TileType.Desert);
            Tiles[new Point(1, 1)] = TileFactory.GetTile(TileType.Plain);
            Tiles[new Point(0, 2)] = TileFactory.GetTile(TileType.Forest);
            Tiles[new Point(0, 3)] = TileFactory.GetTile(TileType.Mountain);
            //Tiles[new Point(-1, 4)] = null;
            Tiles[new Point(-1, 5)] = TileFactory.GetTile(TileType.Plain);

            Tiles[new Point(2, 0)] = TileFactory.GetTile(TileType.Desert);
            Tiles[new Point(2, 1)] = TileFactory.GetTile(TileType.Plain);
            Tiles[new Point(1, 2)] = TileFactory.GetTile(TileType.Mountain);
            Tiles[new Point(1, 3)] = TileFactory.GetTile(TileType.Forest);
            Tiles[new Point(0, 4)] = TileFactory.GetTile(TileType.Plain);
            Tiles[new Point(0, 5)] = TileFactory.GetTile(TileType.Plain);

            //Tiles[new Point(3, 0)] = null;
            Tiles[new Point(3, 1)] = TileFactory.GetTile(TileType.Desert);
            Tiles[new Point(2, 2)] = TileFactory.GetTile(TileType.Plain);
            Tiles[new Point(2, 3)] = TileFactory.GetTile(TileType.Forest);
            Tiles[new Point(1, 4)] = TileFactory.GetTile(TileType.Forest);
            Tiles[new Point(1, 5)] = TileFactory.GetTile(TileType.Desert);

            Tiles[new Point(4, 0)] = TileFactory.GetTile(TileType.Desert);
            Tiles[new Point(4, 1)] = TileFactory.GetTile(TileType.Sea);
            Tiles[new Point(3, 2)] = TileFactory.GetTile(TileType.Sea);
            //Tiles[new Point(3, 3)] = null;
            Tiles[new Point(2, 4)] = TileFactory.GetTile(TileType.Plain);
            Tiles[new Point(2, 5)] = TileFactory.GetTile(TileType.Plain);

            Tiles[new Point(5, 0)] = TileFactory.GetTile(TileType.Plain);
            Tiles[new Point(5, 1)] = TileFactory.GetTile(TileType.Forest);
            Tiles[new Point(4, 2)] = TileFactory.GetTile(TileType.Sea);
            //Tiles[new Point(4, 3)] = null;
            //Tiles[new Point(3, 4)] = null
            Tiles[new Point(3, 5)] = TileFactory.GetTile(TileType.Plain);

            Tiles[new Point(6, 0)] = TileFactory.GetTile(TileType.Plain);
            Tiles[new Point(6, 1)] = TileFactory.GetTile(TileType.Mountain);
            //Tiles[new Point(5, 2)] = null;
            //Tiles[new Point(5, 3)] = null;
            Tiles[new Point(4, 4)] = TileFactory.GetTile(TileType.Sea);
            Tiles[new Point(4, 5)] = TileFactory.GetTile(TileType.Desert);

            Tiles[new Point(7, 0)] = TileFactory.GetTile(TileType.Mountain);
            Tiles[new Point(7, 1)] = TileFactory.GetTile(TileType.Mountain);
            //Tiles[new Point(6, 2)] = null;
            //Tiles[new Point(6, 3)] = null;
            Tiles[new Point(5, 4)] = TileFactory.GetTile(TileType.Desert);
            Tiles[new Point(5, 5)] = TileFactory.GetTile(TileType.Desert);
        }
    }
}
