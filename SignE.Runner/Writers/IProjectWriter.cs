using SignE.Core.Levels;
using SignE.Runner.Models;

namespace SignE.Runner.Writers
{
    public interface IProjectWriter
    {
        void WriteProject(Project project, string filepath);
        void WriteLevel(Level level, string filepath);
    }
}