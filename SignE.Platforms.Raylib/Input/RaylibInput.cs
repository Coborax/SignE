using Raylib_cs;
using SignE.Core.Input;

namespace SignE.Platforms.RayLib.Input
{
    public class RaylibInput : IInput
    {
        public bool IsKeyDown(Key key)
        {
            return Raylib.IsKeyDown((KeyboardKey)key);
        }
    }
}