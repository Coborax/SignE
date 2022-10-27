using System;
using SignE.Core;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;
using SignE.Core.ECS.Systems;
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
            
            Entity entity = new Entity();
            entity.AddComponent(new Position2DComponent(0, 0));
            entity.AddComponent(new SpriteComponent("Resources/rpg-pack/chars/gabe/gabe-idle-run.png", 24, 24));
            entity.AddComponent(new Movement2DComponent());
            entity.AddComponent(new YSortComponent());
            entity.AddComponent(new Camera2DComponent());
            world.AddEntity(entity);

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
                    world.AddEntity(entity);
                }
            }
            
            

            world.RegisterSystem(new Draw2DSystem());
            world.RegisterSystem(new Movement2DSystem());
            world.RegisterSystem(new YSortSystem());
            world.RegisterSystem(new Camera2DSystem());
            
            Core.SignE.Graphics.Camera2D.Zoom = 3;
            
            game.Run();
        }
    }
}