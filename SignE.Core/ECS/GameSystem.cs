using System;
using System.Collections.Generic;
using SignE.Core.Extensions;

namespace SignE.Core.ECS
{
    public abstract class GameSystem
    {
        protected List<Entity> Entities;

        public abstract void UpdateSystem();
        public abstract void LateUpdateSystem();
        public abstract void DrawSystem();
        public abstract void GetEntities(World world);
    }
}