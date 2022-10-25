using System;
using System.Collections.Generic;

namespace SignE.Core.ECS
{
    public class World
    {
        public List<Entity> Entities { get; private set; } = new List<Entity>();
        private List<IGameSystem> _systems = new List<IGameSystem>();

        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
        }

        public void RegisterSystem(IGameSystem gameSystem)
        { 
            _systems.Add(gameSystem);  
        }

        public void UpdateSystems()
        {
            foreach (var gameSystem in _systems)
            {
                gameSystem.UpdateSystem(this);
            }
        }

        public void DrawSystems()
        {
            foreach (var gameSystem in _systems)
            {
                gameSystem.DrawSystem(this);
            }
        }
    }
}