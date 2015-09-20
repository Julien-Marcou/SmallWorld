using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SmallWorld.Models.AI
{
    [DataContract()]
    public class DijkstraAI : ArtificialIntelligence
    {
        const int Infinity = -1;
        
        private static Dictionary<Type, Dictionary<Point, Dictionary<Point, double>>> roadmaps;

        [OnDeserializing]
        private void OnDeserializing(StreamingContext c)
        {
            roadmaps = new Dictionary<Type, Dictionary<Point, Dictionary<Point, double>>>();
        }

        public DijkstraAI(Game game, Player player) : base(game, player)
        {
            roadmaps = new Dictionary<Type, Dictionary<Point, Dictionary<Point, double>>>();
        }

        protected override KeyValuePair<Point, Unit> SelectOrigin(List<KeyValuePair<Point, Unit>> origins)
        {
            return origins.First();
        }

        protected override Point SelectDestination(Unit unit, Point origin, List<Point> destinations)
        {
            // Recovers the Roadmap (graph of links between tiles) for the selected Unit
            var unitType = unit.GetType();
            if (!roadmaps.ContainsKey(unitType))
            {
                roadmaps.Add(unitType, Game.Map.GetRoadmap(unit));
            }
            var roadmap = roadmaps[unitType];

            // Find best path
            Tuple<double, List<Point>> bestPath = null;
            var targets = GetTargets();
            foreach (var target in targets)
            {
                var path = Dijkstra(roadmap, origin, target);
                if ((path.Item1 != Infinity) && (bestPath == null || path.Item1 < bestPath.Item1))
                {
                    bestPath = path;
                }
            }

            // If no path to any targets
            if (bestPath == null)
            {
                // Returns one possible destination
                return destinations.First();
            }

            // Else, returns the first step of the path
            return bestPath.Item2.First();
        }

        private List<Point> GetTargets()
        {
            var targets = new List<Point>();

            // Foreach enemy Players
            foreach (var intPlayerPair in Game.Players)
            {
                if (intPlayerPair.Value != Player)
                {
                    // Foreach enemy Units
                    var enemyUnits = intPlayerPair.Value.GetUnits();
                    foreach (var unit in enemyUnits)
                    {
                        // Add coordinates
                        targets.Add(unit.Key);
                    }
                }
            }

            return targets;
        }

        private Tuple<double, List<Point>> Dijkstra(Dictionary<Point, Dictionary<Point, double>> roadmap, Point origin, Point destination)
        {
            var unvisitedNodes = new List<Point>();
            var previousNode = new Dictionary<Point, Point>();
            var distanceOrigin = new Dictionary<Point, double>();

            foreach (var tile in Game.Map.Tiles)
            {
                var node = tile.Key;
                if (node.Equals(origin))
                {
                    distanceOrigin[node] = 0;
                }
                else
                {
                    distanceOrigin[node] = Infinity;
                }
                previousNode[node] = null;
                unvisitedNodes.Add(node);
            }

            while (unvisitedNodes.Count > 0)
            {
                Point bestNode = null;
                foreach (var node in unvisitedNodes)
                {
                    if ((distanceOrigin[node] != Infinity) && (bestNode == null || distanceOrigin[node] < distanceOrigin[bestNode]))
                    {
                        bestNode = node;
                    }
                }
                if(bestNode == null || bestNode.Equals(destination))
                {
                    break;
                }
                unvisitedNodes.Remove(bestNode);

                var neighbors = roadmap[bestNode];
                foreach (var neighbor in neighbors)
                {
                    var node = neighbor.Key;
                    var cost = neighbor.Value;
                    if (unvisitedNodes.Contains(node))
                    {
                        var distance = distanceOrigin[bestNode] + cost;
                        if ((distance != Infinity) && (distanceOrigin[node] == Infinity || distance < distanceOrigin[node]))
                        {
                            distanceOrigin[node] = distance;
                            previousNode[node] = bestNode;
                        }
                    }
                }
            }

            var path = new List<Point>();
            var current = destination;
            while (!current.Equals(origin) && previousNode[current] != null)
            {
                path.Insert(0, current);
                current = previousNode[current];
            }

            return new Tuple<double, List<Point>>(distanceOrigin[destination], path);
        }
    }
}
