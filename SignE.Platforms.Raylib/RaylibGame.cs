using System;
using System.Numerics;
using SignE.Core;
using Raylib_cs;
using SignE.Core.ECS;
using SignE.Platforms.RayLib.Graphics;
using SignE.Platforms.RayLib.Input;

namespace SignE.Platforms.RayLib
{
    public class RaylibGame : Game
    {
        public RaylibGame()
        {
            Core.SignE.Graphics = new RaylibGraphics();
            Core.SignE.Input = new RaylibInput();
        }
        
        public override void Init(int w, int h, string title, World world)
        {
            base.Init(w, h, title, world);
            
            Raylib.InitWindow(w, h, title);
            Raylib.SetTargetFPS(60);
            
            ((RaylibCamera2D)Core.SignE.Graphics.Camera2D).SetOffsetCenter(w / 2, h / 2);
        }

        protected override void Loop()
        {
            while (!Raylib.WindowShouldClose())
            {
                World.UpdateSystems();
                
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RAYWHITE);

                Raylib.BeginMode2D(((RaylibCamera2D)Core.SignE.Graphics.Camera2D).Camera2D);
                
                World.DrawSystems();
                
                Raylib.EndMode2D();
                
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