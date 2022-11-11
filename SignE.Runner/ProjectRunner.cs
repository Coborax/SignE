using SignE.Core;
using SignE.Core.ECS;
using SignE.Core.ECS.Systems;
using SignE.Runner.Readers;

namespace SignE.Runner
{
    public static class ProjectRunner
    {
        private static readonly IProjectReader ProjectReader = new JsonProjectReader();
        
        public static void SetupGame(Game game)
        {
            var project = ProjectReader.ReadProject("project.json");
            var world = new World();
            
            game.Init(project.WindowWidth, project.WindowHeight, project.ProjectName, world);
            
            foreach (var projectLevel in project.ProjectLevels)
            {
                Core.SignE.LevelManager.AddLevel(ProjectReader.ReadLevel<JsonLevel>(projectLevel));
            }
            Core.SignE.LevelManager.LoadLevel(project.StartupLevel);
            SignE.Core.SignE.Graphics.Camera2D.Zoom = 3;
        }
    }
}