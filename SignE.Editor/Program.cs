using SignE.Core.ECS;
using SignE.Editor.ECS.Systems;
using SignE.Platforms.RayLib;

namespace SignE.Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            var editor = new RaylibGame();
            var world = new World();

            world.RegisterSystem(new EditorSystem());
            
            editor.Init(1280, 720, "SignE Editor", world);
            editor.Run();
        }
    }
}