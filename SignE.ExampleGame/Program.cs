using System;
using SignE.Core;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;
using SignE.Core.ECS.Systems;
using SignE.Core.Levels;
using SignE.Core.Levels.Ldtk;
using SignE.ExampleGame.ECS.Components;
using SignE.ExampleGame.ECS.Systems;
using SignE.Platforms.RayLib;

namespace SignE.ExampleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new RaylibGame();
            Runner.Runner.SetupGame(game);
            game.Run();
        }
    }
}