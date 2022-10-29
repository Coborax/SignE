using SignE.Runner.Models;

namespace SignE.Runner.Readers
{
    public interface IProjectReader
    {
        Project ReadProject(string path);
    }
}