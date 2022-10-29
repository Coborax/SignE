using System;
using System.Numerics;
using ImGuiNET;
using SignE.Core;
using Raylib_cs;
using rlImGui_cs;
using SignE.Core.ECS;
using SignE.Platforms.RayLib.Graphics;
using SignE.Platforms.RayLib.Input;

namespace SignE.Platforms.RayLib
{
    public class RaylibGame : Game
    {
        private RenderTexture2D _renderTexture2D;
        
        public RaylibGame()
        {
            Core.SignE.Graphics = new RaylibGraphics();
            Core.SignE.Input = new RaylibInput();
        }
        
        public override void Init(int w, int h, string title, World world)
        {
            base.Init(w, h, title, world);
            
            Raylib.InitWindow(w, h, title);
            //Raylib.SetTargetFPS(60);
            
            //((RaylibCamera2D)Core.SignE.Graphics.Camera2D).SetOffsetCenter(w / 2, h / 2);

            _renderTexture2D = Raylib.LoadRenderTexture(w, h);
            ((RaylibGraphics) Core.SignE.Graphics).RenderTexture2D = _renderTexture2D;
            
            Core.SignE.Graphics.InitImGui();
        }

        protected override void Loop()
        {
            while (!Raylib.WindowShouldClose())
            {
                World.UpdateSystems();

                if (Core.SignE.LevelManager.CurrentLevel != null)
                    Core.SignE.LevelManager.CurrentLevel.World.UpdateSystems();

                if (RenderGameToTexture)
                {
                    Raylib.BeginTextureMode(_renderTexture2D);
                    DrawGame();
                    Raylib.EndTextureMode();
                }
                
                Raylib.BeginDrawing();
                
                if (!RenderGameToTexture)
                    DrawGame();

                Core.SignE.Graphics.BeginImGui();

                if (Core.SignE.Graphics.ImGui != null)
                {
                    Core.SignE.Graphics.ImGui.SubmitUi();
                }

                Core.SignE.Graphics.EndImGui();
                
                Raylib.EndDrawing();
            }
        }

        private void DrawGame()
        {
            Raylib.ClearBackground(new Color(24, 20, 37, 255));
            
            Raylib.BeginMode2D(((RaylibCamera2D)Core.SignE.Graphics.Camera2D).Camera2D);
                
            World.DrawSystems();
            if (Core.SignE.LevelManager.CurrentLevel != null)
                Core.SignE.LevelManager.CurrentLevel.World.DrawSystems();
                
            Raylib.EndMode2D();
            
            Raylib.DrawFPS(10, 10);
        }


        public override void Dispose()
        {
            Core.SignE.Graphics.CleanupImGui();

            Raylib.UnloadRenderTexture(_renderTexture2D);
            Raylib.CloseWindow();
            GC.SuppressFinalize(this);
        }
    }
}