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
            entity.AddComponent(new Position2DComponent(10, 10));
            entity.AddComponent(new SpriteComponent("Resources/torben.png", 10));
            entity.AddComponent(new Movement2DComponent());
            world.AddEntity(entity);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    entity = new Entity();
                    entity.AddComponent(new Position2DComponent(10 + 300 * i, 10 + + 300 * j));
                    entity.AddComponent(new SpriteComponent("Resources/torben.png"));
                    world.AddEntity(entity);   
                }
            }

            world.RegisterSystem(new DrawGameSystem());
            world.RegisterSystem(new Movement2DSystem());
            world.RegisterSystem(new YSortSystem());
            
            game.Run();
        }
    }
}