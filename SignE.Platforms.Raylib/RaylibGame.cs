using System;
using SignE.Core;
using Raylib_cs;
using SignE.Core.ECS;
using SignE.Platforms.RayLib.Graphics;
using SignE.Platforms.RayLib.Input;

namespace SignE.Platforms.RayLib
{
    public class RaylibGame : Game
    {
        protected override void Init(int w, int h, string title, World world)
        {
            base.Init(w, h, title, world);
            Core.SignE.Graphics = new RaylibGraphics();
            Core.SignE.Input = new RaylibInput();
            
            Raylib.InitWindow(w, h, title);
            Raylib.SetTargetFPS(60);
        }

        protected override void Loop()
        {
            while (!Raylib.WindowShouldClose())
            {
                World.UpdateSystems();
                
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RAYWHITE);

                World.DrawSystems();
                
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