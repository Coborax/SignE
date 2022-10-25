using System;
using System.Collections.Generic;

namespace SignE.Core.ECS
{
    public class Entity
    {
        public Guid Id { get; } = Guid.NewGuid();
        public List<IComponent> Components { get; } = new List<IComponent>();
    }
}