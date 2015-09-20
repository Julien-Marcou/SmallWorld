using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SmallWorld.Models.AI
{
    [DataContract()]
    public class RandomAI : ArtificialIntelligence
    {
        private Random randomizer;

        [OnDeserializing]
        private void OnDeserializing(StreamingContext c)
        {
            randomizer = new Random();
        }

        public RandomAI(Game game, Player player) : base(game, player)
        {
            randomizer = new Random();
        }

        protected override KeyValuePair<Point, Unit> SelectOrigin(List<KeyValuePair<Point, Unit>> origins)
        {
            return origins[randomizer.Next(origins.Count())];
        }

        protected override Point SelectDestination(Unit unit, Point origin, List<Point> destinations)
        {
            return destinations[randomizer.Next(destinations.Count())];
        }
    }
}
