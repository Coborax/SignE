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
            World world = new World();
            
            game.Init(1280, 720, "SignE Example Game", world);
            
            Core.SignE.LevelManager.AddLevel(new LdtkLevel("Resources/ExampleWorld.ldtk", "Level_0"));
            Core.SignE.LevelManager.LoadLevel("Level_0");
            Core.SignE.Graphics.Camera2D.Zoom = 3;    
            
            game.Run();
        }
    }
}