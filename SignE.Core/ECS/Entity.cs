using System;
using System.Collections.Generic;
using System.Linq;

namespace SignE.Core.ECS
{
    public class Entity
    {
        public Guid Id { get; } = Guid.NewGuid();
        private List<IComponent> _components = new List<IComponent>();

        public void AddComponent(IComponent component)
        {
            _components.Add(component);
        }
        
        public bool HasComponent<T>()
        {
            return _components.OfType<T>().Any();
        }

        public T GetComponent<T>() where T : IComponent
        {
            return (T)_components.Find(c => c is T);
        }
    }
}