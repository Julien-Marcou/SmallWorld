using SmallWorld.Models.AI;
using SmallWorld.Models.Factions;
using SmallWorld.Models.Interfaces;
using SmallWorld.Models.Maps;
using SmallWorld.Models.Tiles;
using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmallWorld.Models
{
    [DataContract(IsReference = true)]
    public class Game : IGame
    {
        [DataMember]
        public Map Map { get; private set; }

        [DataMember]
        public Dictionary<int, Player> Players { get; private set; }
        
        public Player CurrentPlayer
        {
            get
            {
                return Players[CurrentPlayerIndex];
            }
            private set
            {
                for (var i = 0; i < Players.Count; i++)
                {
                    if (Players[i] == value)
                    {
                        CurrentPlayerIndex = i;
                        break;
                    }
                }
            }
        }

        [DataMember]
        public int CurrentPlayerIndex { get; private set; }

        [DataMember]
        public int CurrentTurn { get; private set; }

        [DataMember]
        public int MaxPlayers { get; private set; }

        [DataMember]
        public int UnitPerPlayer { get; private set; }

        [DataMember]
        public int MaxTurns { get; private set; }

        [DataMember]
        public bool IsFinished { get; private set; }

        public Game(MapType mapType) : this(mapType, Guid.NewGuid().GetHashCode())
        {
        }

        public Game(MapType mapType, int mapSeed)
        {
            Map = MapFactory.GetMap(mapType, mapSeed);
            Players = new Dictionary<int, Player>();
            CurrentPlayerIndex = 0;
            CurrentTurn = 0;
            MaxPlayers = Map.MaxPlayers;
            UnitPerPlayer = Map.UnitPerPlayer;
            MaxTurns = Map.MaxTurns;
            IsFinished = false;
        }

        public void AddPlayer(string name, Color color, FactionType factionType, AIType aiType = AIType.None)
        {
            if (Players.Count == MaxPlayers)
            {
                throw new InvalidOperationException("Current Game only supports " + MaxPlayers + " players");
            }

            var id = Players.Count;
            var spawn = Map.GetSpawn(id);
            var faction = FactionFactory.GetFaction(factionType, UnitPerPlayer, spawn);
            var player = new Player(name, color, spawn, faction);
            if(aiType != AIType.None)
            {
                var ai = AIFactory.GetAI(aiType, this, player);
                player.SetAI(ai);
            }
            Players.Add(id, player);
            Map.PlayerControlsTile(player, spawn);
        }

        public bool MoveUnit(Unit unit, Point origin, Point destination)
        {
            if (!IsFinished && CurrentPlayer == Map.GetTileController(origin) && origin != destination && Map.IsReachableTile(unit, origin, destination))
            {
                bool executeMovement = false;

                // If the destination tile is not controlled, or controlled by the same CurrentPlayer
                if (!Map.TileIsControlled(destination) || Map.TileIsControlledBy(destination, CurrentPlayer))
                {
                    // The Player can move to the destination tile
                    executeMovement = true;
                }
                // Else, the unit need to fight before move
                else
                {
                    // Get fighting Players
                    Player attackerPlayer = CurrentPlayer;
                    Player defenderPlayer = Map.GetTileController(destination);

                    // Get fighting Units
                    Unit attacker = unit;
                    Unit defender = defenderPlayer.GetBestUnitOn(destination);

                    // Fight
                    var fightIssue = attacker.Fight(defender);

                    // Attacker died
                    if (fightIssue == FightIssue.Lose)
                    {
                        attackerPlayer.RemoveUnit(attacker, origin);

                        // If there is no more unit on the star tile
                        if (!attackerPlayer.HasUnitsOn(origin))
                        {
                            // Releases the tile
                            Map.PlayerReleasesTile(attackerPlayer, origin);
                        }
                    }
                    // Defender died
                    else if (fightIssue == FightIssue.Win)
                    {
                        defenderPlayer.RemoveUnit(defender, destination);

                        // If there is no more unit on the star tile
                        if (!defenderPlayer.HasUnitsOn(destination))
                        {
                            // Releases the tile
                            Map.PlayerReleasesTile(defenderPlayer, destination);

                            // The attacker can finaly move to the destination tile
                            executeMovement = true;
                        }
                    }

                    // If there is a winner to the fight, check if there is a winner for the game
                    if (fightIssue != FightIssue.Draw && CheckForAWinner())
                    {
                        EndOfTheGame();
                    }
                }

                // Move the unit (decrease its movement points but does not move it on the map)
                unit.Move(Map.Tiles[origin], Map.Tiles[destination], Map.AreAdjacent(origin, destination));

                // Move the unit (physically on the map)
                if (executeMovement)
                {
                    CurrentPlayer.MoveUnit(unit, origin, destination);
                    if (!CurrentPlayer.HasUnitsOn(origin))
                    {
                        Map.PlayerReleasesTile(CurrentPlayer, origin);
                    }
                    Map.PlayerControlsTile(CurrentPlayer, destination);
                }

                return true;
            }

            return false;
        }

        public void NextPlayer()
        {
            if (!IsFinished)
            {
                // Reset the current player (basicaly, reset movement points of his units)
                CurrentPlayer.ResetMovementPoints();

                // Calculate the position of the next player
                int nextPlayerIndex = (CurrentPlayerIndex + 1) % MaxPlayers;

                // If the next player is the first, it's a new turn
                if (nextPlayerIndex == 0)
                {
                    CurrentTurn++;

                    // If max turns is reached, it's the end of the game
                    if (CurrentTurn == MaxTurns)
                    {
                        EndOfTheGame();
                    }
                }

                // Gives the hand to the next player
                CurrentPlayerIndex = nextPlayerIndex;
            }
        }

        public List<Player> GetWinners()
        {
            List<Player> winners = new List<Player>();
            if (IsFinished)
            {
                int maxScore = -1;
                foreach (var intPlayerPair in Players)
                {
                    Player player = intPlayerPair.Value;
                    int playerScore = player.Score;
                    if (playerScore > maxScore)
                    {
                        winners.Clear();
                        winners.Add(player);
                        maxScore = playerScore;
                    }
                    else if (playerScore == maxScore)
                    {
                        winners.Add(player);
                    }
                }
            }
            return winners;
        }

        public Dictionary<Point, List<Unit>> GetUnits()
        {
            var units = new Dictionary<Point, List<Unit>>();
            foreach (var intPlayerPair in Players)
            {
                foreach (var tileUnits in intPlayerPair.Value.GetUnits())
                {
                    if (!units.ContainsKey(tileUnits.Key))
                    {
                        units.Add(tileUnits.Key, tileUnits.Value);
                    }
                    else
                    {
                        units[tileUnits.Key].AddRange(tileUnits.Value);
                    }
                }
            }
            return units;
        }

        public Dictionary<Point, Tile> GetTiles()
        {
            return Map.Tiles;
        }

        private bool CheckForAWinner()
        {
            int playersWithRemainingUnits = 0;
            foreach (var intPlayerPair in Players)
            {
                if (intPlayerPair.Value.HasUnits())
                {
                    playersWithRemainingUnits++;
                }
            }
            return playersWithRemainingUnits == 1;
        }

        private void EndOfTheGame()
        {
            IsFinished = true;
        }
    }
}
