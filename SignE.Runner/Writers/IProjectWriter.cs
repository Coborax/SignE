using SignE.Runner.Models;

namespace SignE.Runner.Readers
{
    public interface IProjectWriter
    {
        void ReadProject(Project project, string filepath);
    }
}