using SmallWorld.Models.Interfaces;
using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SmallWorld.Models.AI
{
    [DataContract()]
    public abstract class ArtificialIntelligence : IArtificialIntelligence
    {
        protected const int BeforePlayDelay = 400;
        protected const int BeforeMoveDelay = 600;
        protected const int AfterMoveDelay = 0;
        protected const int AfterPlayDelay = 200;

        [DataMember]
        public Game Game { get; protected set; }

        [DataMember]
        public Player Player { get; protected set; }

        public ArtificialIntelligence(Game game, Player player)
        {
            Game = game;
            Player = player;
        }

        public async virtual Task Play(Action beforePlay = null, Action beforeMove = null, Action afterMove = null, Action afterPlay = null)
        {
            await BeforePlay(beforePlay);

            var movableUnits = GetMovableUnits();
            while (!Game.IsFinished && movableUnits.Count > 0)
            {
                var origin = SelectOrigin(movableUnits);
                var reachableTiles = Game.Map.GetReachableTiles(origin.Value, origin.Key);
                var destination = SelectDestination(origin.Value, origin.Key, reachableTiles);
                
                await BeforeMove(beforeMove);

                // If it ever doesn't want to play one of the proposed destinations
                if (destination == null || !reachableTiles.Contains(destination))
                {
                    origin.Value.Pass();
                }
                else
                {
                    Game.MoveUnit(origin.Value, origin.Key, destination);
                }

                await AfterMove(afterMove);

                movableUnits = GetMovableUnits();
            }
            Game.NextPlayer();

            await AfterPlay(afterPlay);
        }

        protected List<KeyValuePair<Point, Unit>> GetMovableUnits()
        {
            var movableUnits = new List<KeyValuePair<Point, Unit>>();

            foreach (var units in Player.GetUnits())
            {
                foreach (var unit in units.Value)
                {
                    var reachableTiles = Game.Map.GetReachableTiles(unit, units.Key);
                    if (reachableTiles.Count > 0)
                    {
                        movableUnits.Add(new KeyValuePair<Point, Unit>(units.Key, unit));
                    }
                }
            }

            return movableUnits;
        }

        protected static void CallAction(Action action)
        {
            if (action != null)
            {
                action.Invoke();
            }
        }

        protected async virtual Task BeforePlay(Action callback)
        {
            CallAction(callback);
            await Task.Delay(BeforePlayDelay);
        }

        protected async virtual Task BeforeMove(Action callback)
        {
            CallAction(callback);
            await Task.Delay(BeforeMoveDelay);
        }

        protected async virtual Task AfterMove(Action callback)
        {
            CallAction(callback);
            await Task.Delay(AfterMoveDelay);
        }

        protected async virtual Task AfterPlay(Action callback)
        {
            CallAction(callback);
            await Task.Delay(AfterPlayDelay);
        }

        protected abstract KeyValuePair<Point, Unit> SelectOrigin(List<KeyValuePair<Point, Unit>> origins);

        protected abstract Point SelectDestination(Unit unit, Point origin, List<Point> destinations);
    }
}
