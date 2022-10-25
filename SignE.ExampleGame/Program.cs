using System;
using SignE.Core;
using SignE.Platforms.RayLib;

namespace SignE.ExampleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new RaylibGame();
            game.Run(1280, 720, "SignE Example Game");
        }
    }
}