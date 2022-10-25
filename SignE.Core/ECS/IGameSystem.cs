using System;
using System.Collections.Generic;
using SignE.Core.Extensions;

namespace SignE.Core.ECS
{
    public interface IGameSystem
    {
        void UpdateSystem(World world);
        void DrawSystem(World world);
    }
}