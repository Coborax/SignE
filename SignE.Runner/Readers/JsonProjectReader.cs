using System.IO;
using Newtonsoft.Json;
using SignE.Core.Levels;
using SignE.Runner.Models;

namespace SignE.Runner.Readers
{
    public class JsonProjectReader : IProjectReader
    {
        public Project ReadProject(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Project>(json);
        }

        public T ReadLevel<T>(string path) where T : JsonLevel, new()
        {
            var json = File.ReadAllText(path);
            
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            
            var levelModel = JsonConvert.DeserializeObject<LevelModel>(json, settings);
            var newLevel = new T
            {
                Name = levelModel.Name,
                LevelModel = levelModel,
                File = path
            };

            return newLevel;
        }
    }
}