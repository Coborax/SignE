using System;
using SignE.Core;
using Raylib_cs;

namespace SignE.Platforms.RayLib
{
    public class RaylibGame : Game
    {
        protected override void Init(int w, int h, string title)
        {
            Raylib.InitWindow(w, h, title);
            Raylib.SetTargetFPS(60);
        }

        protected override void Loop()
        {
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RAYWHITE);

                Raylib.DrawFPS(10, 10);

                Raylib.EndDrawing();
            }
        }

        public override void Dispose()
        {
            Raylib.CloseWindow();
            GC.SuppressFinalize(this);
        }
    }
}