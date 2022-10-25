using System;
using System.Collections.Generic;

namespace SignE.Core.ECS
{
    public class World
    {
        private List<Entity> _entities = new List<Entity>();
        private List<GameSystem> _systems = new List<GameSystem>();

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }

        public void RegisterSystem(GameSystem gameSystem)
        {
            _systems.Add(gameSystem);
        }


        public List<ComponentPairs> GetComponentPairs(List<Type> componentTypes)
        {
            throw new NotImplementedException();
        }
    }
}