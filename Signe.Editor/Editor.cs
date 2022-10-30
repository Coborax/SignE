using System.Collections.Generic;
using SignE.Core.ECS;
using SignE.Core.Levels;
using SignE.Runner.Models;
using SignE.Runner.Readers;
using SignE.Runner.Writers;

namespace Signe.Editor
{
    public class Editor
    {
        public static Editor MainEditor { get; set; } = new Editor();
        
        public Project Project { get; set; }
        public List<Level> Levels => SignE.Core.SignE.LevelManager.GetLevelList();
        public Level CurrentLevel => SignE.Core.SignE.LevelManager.CurrentLevel;
        
        public Entity SelectedEntity { get; set; }

        private IProjectWriter _projectWriter = new JsonProjectWriter();
        private IProjectReader _projectReader = new JsonProjectReader();
        
        public void CreateNewProject()
        {
            var project = new Project
            {
                ProjectName = "NewProject"
            };
            _projectWriter.ReadProject(project, "test.json");
        }
        
        public void LoadProject()
        {
            Project = _projectReader.ReadProject("test.json");
        }

        public void LoadLevel(Level level)
        {
            if(CurrentLevel == level)
                return;

            SignE.Core.SignE.LevelManager.LoadLevel(level.Name);
        }
    }
}