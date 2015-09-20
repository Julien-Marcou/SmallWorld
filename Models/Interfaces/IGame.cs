using SmallWorld.Models.AI;
using SmallWorld.Models.Factions;
using SmallWorld.Models.Maps;
using SmallWorld.Models.Tiles;
using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System;
using System.Collections.Generic;

namespace SmallWorld.Models.Interfaces
{
    interface IGame
    {
        Map Map { get; }

        Dictionary<int, Player> Players { get; }

        int CurrentPlayerIndex { get; }

        int CurrentTurn { get; }

        int MaxPlayers { get; }

        int UnitPerPlayer { get; }

        int MaxTurns { get; }

        bool IsFinished { get; }

        void AddPlayer(string name, Color color, FactionType factionType, AIType aiType = AIType.None);

        bool MoveUnit(Unit unit, Point start, Point stop);

        void NextPlayer();

        Dictionary<Point, List<Unit>> GetUnits();

        Dictionary<Point, Tile> GetTiles();

        List<Player> GetWinners();
    }
}
