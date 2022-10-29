using System;
using SignE.Core;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;
using SignE.Core.ECS.Systems;
using SignE.Core.Levels;
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
            
            SignE.Core.SignE.LevelManager.AddLevel(new TestLevel("Level_1"));
            SignE.Core.SignE.LevelManager.LoadLevel("Level_1");

            SignE.Core.SignE.Graphics.Camera2D.Zoom = 3;
            
            game.Run();
        }
    }

    class TestLevel : Level
    {
        public TestLevel(string name)
        {
            Name = name;
        }
        
        public override string Name { get; set; }
        public override void LoadLevel()
        {
            World = new World();

            var ent = new Entity();
            ent.AddComponent(new Position2DComponent(100, 100));
            ent.AddComponent(new CircleComponent(10.0f));
            World.AddEntity(ent);
            
            ent = new Entity();
            ent.AddComponent(new Position2DComponent(150, 100));
            ent.AddComponent(new RectangleComponent(10, 40));
            World.AddEntity(ent);
            
            ent = new Entity();
            ent.AddComponent(new Position2DComponent(200, 100));
            ent.AddComponent(new SpriteComponent("Resources/cavesofgallet_tiles.png"));
            World.AddEntity(ent);
            
            World.RegisterSystem(new Draw2DSystem());
        }
    }
}