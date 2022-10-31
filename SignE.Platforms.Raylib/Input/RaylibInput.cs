using System;
using Raylib_cs;
using SignE.Core.Input;
using MouseButton = Raylib_cs.MouseButton;

namespace SignE.Platforms.RayLib.Input
{
    public class RaylibInput : IInput
    {
        public bool IsInputActive { get; set; }

        public bool IsKeyDown(Key key)
        {
            return IsInputActive ? Raylib.IsKeyDown((KeyboardKey) key) : false;
        }

        public bool IsKeyPressed(Key key)
        {
            return IsInputActive ? Raylib.IsKeyPressed((KeyboardKey) key) : false;
        }

        public bool IsMouseButtonDown(Core.Input.MouseButton mouseButton)
        {
            return IsInputActive ? Raylib.IsMouseButtonDown((MouseButton) mouseButton) : false;
        }

        public float GetMouseWheelMove()
        {
            return IsInputActive ? Raylib.GetMouseWheelMove() : 0.0f;
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