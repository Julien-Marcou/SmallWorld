using SmallWorld.Models;
using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using SmallWorld.ViewModels.Utils;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SmallWorld.ViewModels
{
    public class MapContext : BaseViewModel
    {
        public Game Game { get; private set; }

        public string Type
        {
            get
            {
                return Game.Map.GetType().Name;
            }
        }

        public Size Size
        {
            get
            {
                return Game.Map.Size;
            }
        }

        public ObservableCollection<TileContext> Tiles
        {
            get
            {
                var tiles = new ObservableCollection<TileContext>();
                foreach (var tile in Game.GetTiles())
                {
                    tiles.Add(new TileContext(tile.Key, tile.Value));
                }
                return tiles;
            }
        }

        public ObservableCollection<Point> ReachableTiles
        {
            get
            {
                if (SelectedUnit != null && SelectedTile != null)
                {
                    return new ObservableCollection<Point>(Game.Map.GetReachableTiles(SelectedUnit, SelectedTile));
                }
                return new ObservableCollection<Point>();
            }
        }

        public ObservableCollection<UnitsContext> Units
        {
            get
            {
                var units = new ObservableCollection<UnitsContext>();
                foreach (var intPlayerPair in Game.Players)
                {
                    foreach (var unitsOnTile in intPlayerPair.Value.GetUnits())
                    {
                        units.Add(new UnitsContext(unitsOnTile.Key, unitsOnTile.Value, intPlayerPair.Value.Color));
                    }
                }
                return units;
            }
        }

        private Point _overedTile = null;
        public Point OveredTile
        {
            get
            {
                return _overedTile;
            }
            private set
            {
                _overedTile = value;
                RaisePropertyChanged();
            }
        }

        private Point _selectedTile = null;
        public Point SelectedTile
        {
            get
            {
                return _selectedTile;
            }
            private set
            {
                _selectedTile = value;
                RaisePropertyChanged();
                RaisePropertyChanged("ReachableTiles");
                RaisePropertyChanged("SelectedUnits");
            }
        }

        public UnitsContext SelectedUnits
        {
            get
            {
                if(SelectedTile != null)
                {
                    foreach (var units in Units)
                    {
                        if (units.Coordinates.Equals(SelectedTile))
                        {
                            return units;
                        }
                    }
                }
                return null;
            }
        }

        private Unit _selectedUnit = null;
        public Unit SelectedUnit
        {
            get
            {
                return _selectedUnit;
            }
            private set
            {
                _selectedUnit = value;
                RaisePropertyChanged();
                RaisePropertyChanged("ReachableTiles");
            }
        }

        public ICommand OverTileCommand
        {
            get
            {
                return new TypedRelayCommand<Point>(tile => OverTile(tile));
            }
        }

        public ICommand SelectTileCommand
        {
            get
            {
                return new TypedRelayCommand<Point>(tile => SelectTile(tile));
            }
        }

        public ICommand SelectUnitCommand
        {
            get
            {
                return new TypedRelayCommand<Unit>(unit => SelectUnit(unit));
            }
        }

        public MapContext(Game game)
        {
            Game = game;
            OveredTile = null;
            SelectedTile = null;
            SelectedUnit = null;
        }

        private void OverTile(Point tile)
        {
            if (tile == null || Game.Map.HasTile(tile))
            {
                OveredTile = tile;
            }
        }

        private void SelectTile(Point tile)
        {
            if (!Game.CurrentPlayer.IsAI() && Game.Map.HasTile(tile))
            {
                // "Select unit" Action
                if (SelectedTile == null && Game.CurrentPlayer.HasUnitsOn(tile))
                {
                    SelectedTile = tile;
                    SelectedUnit = Game.CurrentPlayer.GetFirstUnitOn(tile);
                    RaisePropertyChanged("SelectUnitAction");
                }
                // "Unselect unit" Action
                else if (SelectedTile == tile)
                {
                    Unselect();
                    RaisePropertyChanged("UnselectUnitAction");
                }
                // "Move unit" Action 
                else if (SelectedTile != null && SelectedUnit != null)
                {
                    MoveUnitTo(tile);
                }
            }
        }

        private void SelectUnit(Unit unit)
        {
            if (!Game.CurrentPlayer.IsAI() && SelectedTile != null)
            {
                var unitsOnSelectedTile = Game.CurrentPlayer.GetUnits()[SelectedTile];
                if (unitsOnSelectedTile.Contains(unit))
                {
                    SelectedUnit = unit;
                }
            }
        }

        private void MoveUnitTo(Point destination)
        {
            if (!Game.CurrentPlayer.IsAI() && SelectedTile != null && SelectedUnit != null)
            {
                var destinationController = Game.Map.GetTileController(destination);
                var destinationHasEnemy = destinationController != null && destinationController != Game.CurrentPlayer;
                var moveSucceed = Game.MoveUnit(SelectedUnit, SelectedTile, destination);
                if (moveSucceed)
                {
                    if(destinationHasEnemy)
                    {
                        RaisePropertyChanged("AttackUnitAction");
                    }
                    else
                    {
                        RaisePropertyChanged("MoveUnitAction");
                    }

                    // Unselects Tile and Unit
                    Unselect();

                    // Notifies of the changes
                    RaiseUnitMove();
                }
            }
        }

        internal void Unselect()
        {
            SelectedTile = null;
            SelectedUnit = null;
        }

        internal void RaiseUnitMove()
        {
            // Properties that could have changed after a Unit Move
            RaisePropertyChanged("Units");
        }
    }
}
