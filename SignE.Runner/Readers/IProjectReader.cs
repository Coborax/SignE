using SignE.Core.Levels;
using SignE.Runner.Models;

namespace SignE.Runner.Readers
{
    public interface IProjectReader
    {
        Project ReadProject(string path);
        T ReadLevel<T>(string path) where T : JsonLevel, new();
    }
}