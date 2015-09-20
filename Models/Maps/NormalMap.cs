using SmallWorld.Models.Utils;
using System.Runtime.Serialization;

namespace SmallWorld.Models.Maps
{
    [DataContract()]
    public class NormalMap : Map
    {
        public NormalMap(int seed) : base(new Size(5, 4), seed)
        {
        }
    }
}
