using System;

namespace SmallWorld.Models.AI
{
    public enum AIType { None, Random, Dijkstra };

    public class AIFactory
    {
        public static ArtificialIntelligence GetAI(AIType type, Game game, Player player)
        {
            switch (type)
            {
                case AIType.None:
                    return null;
                case AIType.Random:
                    return new RandomAI(game, player);
                case AIType.Dijkstra:
                    return new DijkstraAI(game, player);
                default:
                    throw new ArgumentException("Impossible to create AI of type \"" + type + "\"", "type");
            }
        }

        public static AIType GetType(ArtificialIntelligence ai)
        {
            if(ai == null)
            {
                return AIType.None;
            }
            var type = ai.GetType().Name;
            switch (type)
            {
                case "RandomAI":
                    return AIType.Random;
                case "DijkstraAI":
                    return AIType.Dijkstra;
                default:
                    throw new ArgumentException("Impossible to retrieve type of AI \"" + type + "\"", "ai");
            }
        }
    }
}
