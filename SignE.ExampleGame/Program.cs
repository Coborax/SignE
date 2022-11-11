using SignE.Core;
using SignE.Runner;
using SignE.Platforms.RayLib;

namespace SignE.ExampleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new RaylibGame();
            ProjectRunner.SetupGame(game);
            game.Run();
        }
    }
}