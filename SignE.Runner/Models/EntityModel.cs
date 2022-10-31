using System;
using System.Collections.Generic;
using SignE.Core.ECS;

namespace SignE.Runner.Models
{
    public class EntityModel
    {
        public Guid Id { get; set; }
        public List<IComponent> Components { get; set; }
    }
}