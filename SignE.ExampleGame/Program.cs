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

            entity = new Entity();
            entity.AddComponent(new Position2DComponent(1280/2, 720/2));
            entity.AddComponent(new SpriteComponent("Resources/rpg-pack/atlas.png"));
            entity.AddComponent(new Movement2DComponent());
            world.AddEntity(entity);

            world.RegisterSystem(new DrawGameSystem());
            world.RegisterSystem(new Movement2DSystem());
            world.RegisterSystem(new YSortSystem());
            
            game.Run();
        }
    }
}