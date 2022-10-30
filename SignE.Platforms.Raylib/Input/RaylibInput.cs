using System;
using Raylib_cs;
using SignE.Core.Input;
using MouseButton = Raylib_cs.MouseButton;

namespace SignE.Platforms.RayLib.Input
{
    public class RaylibInput : IInput
    {
        public bool IsKeyDown(Key key)
        {
            return Raylib.IsKeyDown((KeyboardKey) key);
        }

        public bool IsKeyPressed(Key key)
        {
            return Raylib.IsKeyPressed((KeyboardKey) key);
        }

        public bool IsMouseButtonDown(Core.Input.MouseButton mouseButton)
        {
            return Raylib.IsMouseButtonDown((MouseButton) mouseButton);
        }

        public float GetMouseWheelMove()
        {
            return Raylib.GetMouseWheelMove();
        }

        public float GetMouseDeltaX()
        {
            return Raylib.GetMouseDelta().X;
        }
        
        public float GetMouseDeltaY()
        {
            return Raylib.GetMouseDelta().Y;
        }
    }
}