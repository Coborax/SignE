using System;
using System.Collections.Generic;

namespace SignE.Core.ECS
{
    public class World
    {
        public List<Entity> Entities { get; private set; } = new List<Entity>();
        private List<GameSystem> _systems = new List<GameSystem>();

        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
            foreach (var system in _systems)
            {
                system.GetEntities(this);
            }
        }
        
        public void RemoveEntity(Entity entity)
        {
            Entities.Remove(entity);
            foreach (var system in _systems)
            {
                system.GetEntities(this);
            }
        }

        public void AddComponent(Entity entity, IComponent component)
        {
            entity.AddComponent(component);
            foreach (var system in _systems)
            {
                system.GetEntities(this);
            }
        }
        
        public void RemoveComponent(Entity entity, IComponent component)
        {
            entity.RemoveComponent(component);
            foreach (var system in _systems)
            {
                system.GetEntities(this);
            }
        }

        public void RegisterSystem(GameSystem gameSystem)
        { 
            gameSystem.GetEntities(this);
            _systems.Add(gameSystem);
        }

        public void UpdateSystems()
        {
            foreach (var gameSystem in _systems)
            {
                gameSystem.UpdateSystem();
            }
        }

        public void LateUpdateSystems()
        {
            foreach (var gameSystem in _systems)
            {
                gameSystem.LateUpdateSystem();
            }
        }
        
        public void DrawSystems()
        {
            foreach (var gameSystem in _systems)
            {
                gameSystem.DrawSystem();
            }
        }

        public List<GameSystem> GetAllSystems()
        {
            return _systems;
        }

        
    }
}