using System.Collections.Generic;
using SignE.Core.ECS;

namespace SignE.Runner.Models
{
    public class LevelModel
    {
        public string Name { get; set; }
        public List<EntityModel> Entities { get; set; }
    }
}