using System;

namespace SmallWorld.Models.Maps
{
    public enum MapType { Small, Normal, Big, Desertic, Vegetal, Swamp, Custom };

    public class MapFactory
    {
        public static Map GetMap(MapType type, int seed)
        {
            switch (type)
            {
                case MapType.Small:
                    return new SmallMap(seed);
                case MapType.Normal:
                    return new NormalMap(seed);
                case MapType.Big:
                    return new BigMap(seed);
                case MapType.Desertic:
                    return new DeserticMap(seed);
                case MapType.Vegetal:
                    return new VegetalMap(seed);
                case MapType.Swamp:
                    return new SwampMap(seed);
                case MapType.Custom:
                    return new CustomMap(seed);
                default:
                    throw new ArgumentException("Impossible to create Map of type \"" + type + "\"", "type");
            }
        }

        public static MapType GetType(Map map)
        {
            var type = map.GetType().Name;
            switch (type)
            {
                case "SmallMap":
                    return MapType.Small;
                case "NormalMap":
                    return MapType.Normal;
                case "BigMap":
                    return MapType.Big;
                case "DeserticMap":
                    return MapType.Desertic;
                case "VegetalMap":
                    return MapType.Vegetal;
                case "SwampMap":
                    return MapType.Swamp;
                case "CustomMap":
                    return MapType.Custom;
                default:
                    throw new ArgumentException("Impossible to retrieve type of Map \"" + type + "\"", "map");
            }
        }
    }
}
