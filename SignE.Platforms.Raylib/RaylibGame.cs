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
            
            
            rlImGui.Setup(true);
            ImGui.GetIO().ConfigFlags = ImGuiConfigFlags.DockingEnable;
        }

        protected override void Loop()
        {
            while (!Raylib.WindowShouldClose())
            {
                World.UpdateSystems();
                Core.SignE.LevelManager.CurrentLevel.World.UpdateSystems();
                
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RAYWHITE);

                Raylib.BeginMode2D(((RaylibCamera2D)Core.SignE.Graphics.Camera2D).Camera2D);
                
                World.DrawSystems();
                Core.SignE.LevelManager.CurrentLevel.World.DrawSystems();
                
                Raylib.EndMode2D();
                
                Raylib.DrawFPS(10, 10);

                rlImGui.Begin();

                if (ImGui.BeginMainMenuBar())
                {
                    if (ImGui.BeginMenu("Projects"))
                    {
                        if (ImGui.MenuItem("Test")) { }
                        ImGui.EndMenu();
                    }
                    
                    if (ImGui.BeginMenu("View"))
                    {
                        if (ImGui.MenuItem("Test")) { }
                        ImGui.EndMenu();
                    }

                    ImGui.EndMainMenuBar();
                }

                rlImGui.End();
                
                Raylib.EndDrawing();
            }
        }

        public override void Dispose()
        {
            rlImGui.Shutdown();
            
            Raylib.CloseWindow();
            GC.SuppressFinalize(this);
        }
    }
}