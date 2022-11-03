using System.Collections.Generic;
using System.IO;
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

        public string ProjectDir { get; set; } = @"C:\Repos\SignE\SignE.ExampleGame";
        public string CurrentLevelPath => ((EditorLevel) SignE.Core.SignE.LevelManager.CurrentLevel).File;

        private IProjectWriter _projectWriter = new JsonProjectWriter();
        private IProjectReader _projectReader = new JsonProjectReader();

        public Editor()
        {
            //SignE.Core.SignE.LevelManager.AddLevel(new EditorLevel("Level_1"));
            //SignE.Core.SignE.LevelManager.LoadLevel("Level_1", true);

            //_projectWriter.WriteLevel(CurrentLevel,$"{ProjectDir}/test.level");
    }
        
        public void CreateNewProject()
        {
            var project = new Project
            {
                ProjectName = "NewProject"
            };
            _projectWriter.WriteProject(project, "test.json");
        }
        
        public void LoadProject()
        {
            Project = _projectReader.ReadProject($"{ProjectDir}/project.json");
        }

        public void LoadLevel(Level level)
        {
            if(CurrentLevel == level)
                return;

            SignE.Core.SignE.LevelManager.LoadLevel(level.Name);
            CurrentLevel.Paused = true;
        }

        public void LoadLevelFromFile(string file)
        {
            var level = _projectReader.ReadLevel<EditorLevel>(file);
            SignE.Core.SignE.LevelManager.AddLevel(level);
            
            LoadLevel(level);
        }

        public void SaveCurrentLevel()
        {
            _projectWriter.WriteLevel(CurrentLevel, CurrentLevelPath);
        }

        public void CloseCurrentLevel()
        {
            //TODO: Show prompt to save when closing instead of just saving
            SaveCurrentLevel();
            SignE.Core.SignE.LevelManager.RemoveLevel(CurrentLevel);
        }
    }
}