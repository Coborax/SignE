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
            World world = new World();
            
            Entity entity = new Entity();
            entity.AddComponent(new Position2DComponent(10, 10));
            entity.AddComponent(new RectangleComponent(200, 100));
            entity.AddComponent(new Movement2DComponent());
            
            world.AddEntity(entity);
            world.RegisterSystem(new DrawGameSystem());
            world.RegisterSystem(new Movement2DSystem());
            
            Game game = new RaylibGame();
            game.Run(1280, 720, "SignE Example Game", world);
        }
    }
}