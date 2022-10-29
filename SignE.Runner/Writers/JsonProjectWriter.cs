using System.IO;
using Newtonsoft.Json;
using SignE.Runner.Models;
using SignE.Runner.Readers;

namespace SignE.Runner.Writers
{
    public class JsonProjectWriter : IProjectWriter
    {
        public void ReadProject(Project project, string filepath)
        {
            var json = JsonConvert.SerializeObject(project);
            File.WriteAllText(filepath, json);
        }
    }
}