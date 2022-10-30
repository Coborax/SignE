using System.Numerics;
using Raylib_cs;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;
using SignE.Core.Graphics;

namespace SignE.Platforms.RayLib.Graphics
{
    public class RaylibCamera2D : ICamera2D
    {
        public Camera2D Camera2D = new Camera2D(Vector2.Zero, Vector2.Zero, 0.0f, 1.0f);

        public float X
        {
            get => Camera2D.target.X; 
            set => Camera2D.target.X = value;
        }

        public float Y
        {
            get => Camera2D.target.Y;
            set => Camera2D.target.Y = value;
        }

        public float Zoom
        {
            get => Camera2D.zoom;
            set
            {
                SetOffsetCenter(Width / 2, Height / 2);
                Camera2D.zoom = value;
            }
        }

        public float Width => Raylib.GetScreenWidth() / Zoom;
        public float Height => Raylib.GetScreenHeight() / Zoom;

        public Entity Target { get; set; }

        public void SetOffsetCenter(float x, float y)
        {
            Camera2D.offset = new Vector2(x, y);
        }
    }
}