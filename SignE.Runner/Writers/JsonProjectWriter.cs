using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SignE.Core.ECS;
using SignE.Core.Levels;
using SignE.Runner.Models;
using SignE.Runner.Readers;

namespace SignE.Runner.Writers
{
    public class JsonProjectWriter : IProjectWriter
    {
        public void WriteProject(Project project, string filepath)
        {
            var json = JsonConvert.SerializeObject(project);
            File.WriteAllText(filepath, json);
        }

        public void WriteLevel(Level level, string filepath)
        {
            var model = new LevelModel
            {
                Name = level.Name,
                Entities = GetEntitiesFromLevel(level)
            };

            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            
            var json = JsonConvert.SerializeObject(model, typeof(IComponent), settings);
            File.WriteAllText(filepath, json);
        }

        private List<EntityModel> GetEntitiesFromLevel(Level level)
        {
            return level.World.Entities.Select(e => new EntityModel
            {
                Id = e.Id,
                Components = e.GetComponents()
            }).ToList();
        }
    }
}