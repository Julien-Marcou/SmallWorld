using SmallWorld.Models.Tiles;
using SmallWorld.Models.Utils;
using SmallWorld.ViewModels.Utils;
using System;

namespace SmallWorld.ViewModels
{
    public class TileContext : BaseViewModel
    {
        private Tile tile;

        public Point Coordinates { get; private set; }

        public string Type
        {
            get
            {
                return tile.GetType().Name + tile.Variant;
            }
        }

        public TileContext(Point coordinates, Tile tile)
        {
            this.tile = tile;
            Coordinates = coordinates;
        }
    }
}