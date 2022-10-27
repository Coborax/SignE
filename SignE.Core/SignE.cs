using SignE.Core.Graphics;
using SignE.Core.Input;
using SignE.Core.Levels;

namespace SignE.Core
{
    public static class SignE
    {
        public static IGraphics Graphics { get; set; }
        public static IInput Input { get; set; }
        public static LevelManager LevelManager { get; set; } = new LevelManager();
    }
}