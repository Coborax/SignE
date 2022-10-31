using System;
using SignE.Core;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;
using SignE.Core.ECS.Systems;
using SignE.Core.Levels;
using Signe.Editor.ECS.Systems;
using SignE.Platforms.RayLib;

namespace Signe.Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new RaylibGame();
            var world = new World();
            
            game.Init(1920, 1080, "SignE Editor", world);
            game.RenderGameToTexture = true;
            
            SignE.Core.SignE.Graphics.ImGui = new EditorImGui();
            SignE.Core.SignE.Graphics.Camera2D.Zoom = 3;
            
            world.RegisterSystem(new EditorControlSystem());
            world.RegisterSystem(new EditorDrawSystem());
            
            game.Run();
        }
    }

    class EditorLevel : Level
    {
        public EditorLevel(string name)
        {
            Name = name;
        }
        
        public override string Name { get; set; }
        public override void LoadLevel()
        {
            World = new World();

            // Register systems needed for editor functionality (Mostly drawing and editor control related systems)
            World.RegisterSystem(new Draw2DSystem());
            World.RegisterSystem(new Movement2DSystem());
            World.RegisterSystem(new YSortSystem());
        }
    }
}