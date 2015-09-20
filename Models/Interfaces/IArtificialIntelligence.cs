using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallWorld.Models.Interfaces
{
    interface IArtificialIntelligence
    {
        Game Game { get; }

        Player Player { get; }

        Task Play(Action beforePlay = null, Action beforeMove = null, Action afterMove = null, Action afterPlay = null);
    }
}
