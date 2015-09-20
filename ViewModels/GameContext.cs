using SmallWorld.Models;
using SmallWorld.ViewModels.Utils;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;

namespace SmallWorld.ViewModels
{
    public class GameContext : BaseViewModel
    {
        private Game _game;
        public Game Game
        {
            get
            {
                return _game;
            }
            private set
            {
                _game = value;
                RaisePropertyChanged("MaxTurns");
                RaiseEndOfTurn();
                RaiseEndOfGame();
                RaisePropertyChanged();
            }
        }

        private MapContext _map;
        public MapContext Map
        {
            get
            {
                return _map;
            }
            private set
            {
                _map = value;
                _map.PropertyChanged += (context, property) =>
                {
                    // If Map has received an "AttackUnitAction", the game is may be over 
                    if (property.PropertyName == "AttackUnitAction" && IsFinished)
                    {
                        RaiseEndOfGame();
                    }
                };
                RaisePropertyChanged();
            }
        }

        public Player CurrentPlayer
        {
            get { return Game.CurrentPlayer; }
        }

        public int CurrentTurn
        {
            get { return Game.CurrentTurn + 1; }
        }

        public int MaxTurns
        {
            get { return Game.MaxTurns; }
        }

        public bool IsFinished
        {
            get { return Game.IsFinished; }
        }

        public List<Player> Players
        {
            get
            {
                // Ordered by their score
                var orderedPlayers = Game.Players.ToList().OrderByDescending(
                    intPlayerPair => intPlayerPair.Value.Score
                );
                return orderedPlayers.Aggregate(
                    new List<Player>(),
                    (list, intPlayerPair) =>
                    {
                        list.Add(intPlayerPair.Value); return list;
                    }
                );
            }
        }

        public List<Player> Winners
        {
            get
            {
                return Game.GetWinners();
            }
        }

        public ICommand EndOfTurnCommand
        {
            get
            {
                return new RelayCommand(() => EndOfTurn(), () => !Game.CurrentPlayer.IsAI());
            }
        }

        public GameContext(Game game)
        {
            Game = game;
            Map = new MapContext(game);
            CheckIA();
        }

        private void EndOfTurn()
        {
            if (!Game.CurrentPlayer.IsAI())
            {
                // Unselects Tile and Unit
                Map.Unselect();

                // Gives the hand to the next Player
                Game.NextPlayer();

                // Notifies of the changes
                if (IsFinished)
                {
                    RaiseEndOfGame();
                }
                else
                {
                    RaiseEndOfTurn();
                    CheckIA();
                }
            }
        }

        private async void CheckIA()
        {
            if (!Game.IsFinished && Game.CurrentPlayer.IsAI())
            {
                await Game.CurrentPlayer.AI.Play(
                    null, // Before play
                    null, // Before move
                    () => // After Move
                    {
                        // Notifies of the changes
                        Map.RaiseUnitMove();

                        if (IsFinished)
                        {
                            RaiseEndOfGame();
                        }
                    },
                    () => // After Play
                    {
                        // Notifies of the changes
                        if (IsFinished)
                        {
                            RaiseEndOfGame();
                        }
                        else
                        {
                            RaiseEndOfTurn();
                            CheckIA();
                        }
                    }
                );
            }
        }

        private void RaiseEndOfTurn()
        {
            // Properties that could have changed with the End of Turn
            RaisePropertyChanged("CurrentTurn");
            RaisePropertyChanged("CurrentPlayer");
            RaisePropertyChanged("EndOfTurnCommand");
        }

        private void RaiseEndOfGame()
        {
            // Properties that could have changed with the End of Game
            RaisePropertyChanged("Players");
            RaisePropertyChanged("Winners");
            RaisePropertyChanged("IsFinished");
        }
    }
}
