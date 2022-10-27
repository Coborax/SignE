using System;
using SignE.Core;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;
using SignE.Core.ECS.Systems;
using SignE.Core.Levels;
using SignE.Platforms.RayLib;

namespace SignE.ExampleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new RaylibGame();
            World world = new World();
            
            game.Init(1280, 720, "SignE Example Game", world);
            
            Core.SignE.LevelManager.AddLevel(new ExampleLevel());
            Core.SignE.LevelManager.LoadLevel("ExampleLevel");
            Core.SignE.Graphics.Camera2D.Zoom = 3;
            
            game.Run();
        }
    }

    class ExampleLevel : Level
    {
        public override string Name { get; set; } = "ExampleLevel";

        public override void LoadLevel()
        {
            World = new World();
            
            Entity entity = new Entity();
            entity.AddComponent(new Position2DComponent(0, 0));
            entity.AddComponent(new SpriteComponent("Resources/rpg-pack/chars/gabe/gabe-idle-run.png", 24, 24));
            entity.AddComponent(new Movement2DComponent());
            entity.AddComponent(new YSortComponent());
            World.AddEntity(entity);

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    entity = new Entity();
                    var sprite = new SpriteComponent("Resources/rpg-pack/atlas.png", 16, 16, -100);
                    sprite.TileX = i;
                    sprite.TileY = j + 16;
                    
                    entity.AddComponent(new Position2DComponent(16 * i, 16 * j));
                    entity.AddComponent(sprite);
                    World.AddEntity(entity);
                }
            }
            
            

            World.RegisterSystem(new Draw2DSystem());
            World.RegisterSystem(new Movement2DSystem());
            World.RegisterSystem(new YSortSystem());
            World.RegisterSystem(new Camera2DSystem());
        }
    }
}