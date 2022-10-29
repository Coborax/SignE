using System.IO;
using Newtonsoft.Json;
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
    }
}