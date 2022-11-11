using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using McMaster.NETCore.Plugins;
using SignE.Core.ECS;
using SignE.Core.Levels;
using SignE.Runner;
using SignE.Runner.Readers;
using SignE.Runner.Writers;
using Project = SignE.Runner.Models.Project;

namespace Signe.Editor
{
    public class Editor
    {
        public static Editor MainEditor { get; set; } = new Editor();
        
        public Project Project { get; set; }
        public List<Level> Levels => SignE.Core.SignE.LevelManager.GetLevelList();
        public Level CurrentLevel => SignE.Core.SignE.LevelManager.CurrentLevel;
        
        public Entity SelectedEntity { get; set; }

        public string ProjectDir { get; set; } = "";
        public string CurrentLevelPath => ((JsonLevel) SignE.Core.SignE.LevelManager.CurrentLevel).File;

        public List<Type> ComponentTypes => _coreTypes.Concat(_gameTypes).ToList();
        
        private List<Type> _coreTypes = new List<Type>();
        private List<Type> _gameTypes = new List<Type>();

        private PluginLoader _componentPluginLoader;
        
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
            
            // Change application working directory, so that we can load assest from the project
            Directory.SetCurrentDirectory(ProjectDir);
            //Assembly.LoadFile($"{ProjectDir}/{Project.AssemblyPath}");
            
            _componentPluginLoader = PluginLoader.CreateFromAssemblyFile($"{ProjectDir}/{Project.AssemblyPath}", sharedTypes: new []{ typeof(IComponent) }, config => config.EnableHotReload = true);
            _componentPluginLoader.Reloaded += (sender, args) =>
            {
                LoadPluginComponents(args.Loader);
                SaveCurrentLevel();
            };
            
            LoadPluginComponents(_componentPluginLoader);
            LoadCoreComponents();
        }

        private void LoadPluginComponents(PluginLoader loader)
        {
            _gameTypes = loader.LoadDefaultAssembly().GetTypes().Where(p => typeof(IComponent).IsAssignableFrom(p)).ToList();
        }

        private void LoadCoreComponents()
        {
            _coreTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IComponent).IsAssignableFrom(p)).ToList();
        }

        public void SaveProject()
        {
            _projectWriter.WriteProject(Project, $"{ProjectDir}/project.json");
        }

        public void LoadLevel(Level level)
        {
            if(CurrentLevel == level)
                return;

            SignE.Core.SignE.LevelManager.LoadLevel(level.Name);
            CurrentLevel.Paused = true;
            
            SelectedEntity = null;
        }

        private void ChangeOutOldGameTypesWithNewGameTypes(Level level)
        {
            //TODO: This is kind of a workaround, maybe look into a better way of doing this
            foreach (var entity in level.World.Entities)
            {
                foreach (var gameType in _gameTypes)
                {
                    var component = entity.GetComponents().FirstOrDefault(c => c.GetType().Name.Equals(gameType.Name));
                    if (component == null)
                        continue;
                    
                    entity.RemoveComponent(component);

                    
                    var newComponent = (IComponent) Activator.CreateInstance(gameType);

                    if (newComponent == null)
                        continue;

                    var oldProps = component.GetType().GetProperties();
                    var newProps = newComponent.GetType().GetProperties();
                    foreach (var oldProp in oldProps)
                    {
                        var newProp = newProps.FirstOrDefault(np => np.Name == oldProp.Name);
                        if (newProp == null)
                            continue;

                        var oldVal = oldProp.GetValue(component);
                        if (oldVal == null)
                            continue;
                        
                        newProp.SetValue(newComponent, oldVal);
                    }
                    
                    entity.AddComponent(newComponent);
                }
            }
        }

        public void LoadLevelFromFile(string file)
        {
            var level = _projectReader.ReadLevel<JsonLevel>(file);
            SignE.Core.SignE.LevelManager.AddLevel(level);
            
            LoadLevel(level);
        }

        public void SaveCurrentLevel()
        {
            ChangeOutOldGameTypesWithNewGameTypes(CurrentLevel);
            _projectWriter.WriteLevel(CurrentLevel, CurrentLevelPath);
        }

        public void CloseCurrentLevel()
        {
            //TODO: Show prompt to save when closing instead of just saving
            SaveCurrentLevel();
            SignE.Core.SignE.LevelManager.RemoveLevel(CurrentLevel);
            SelectedEntity = null;
        }

        public void RunGame()
        {
            var p = new Process(); 
            p.StartInfo = new ProcessStartInfo("dotnet")
            {
                Arguments = $@"run"
            };
            p.Start();
        }

        public void BuildGame()
        {
            var p = new Process(); 
            p.StartInfo = new ProcessStartInfo("dotnet")
            {
                Arguments = $@"build"
            };
            p.Start();
        }
    }
}