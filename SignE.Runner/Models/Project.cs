using System.Collections.Generic;
using SignE.Core.Levels;

namespace SignE.Runner.Models
{
    public class Project
    {
        public string ProjectName { get; set; }
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }

        public List<string> ProjectLevels { get; set; } = new List<string>();
        public string StartupLevel { get; set; }

        public string AssemblyPath { get; set; }
    }
}