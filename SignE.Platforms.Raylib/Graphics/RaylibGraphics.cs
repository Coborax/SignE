using System.Collections.Generic;
using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using SignE.Core.Graphics;

namespace SignE.Platforms.RayLib.Graphics
{
    public class RaylibGraphics : IGraphics
    {
        private Dictionary<string, ISprite> _loadedSprites = new Dictionary<string, ISprite>();
        public ICamera2D Camera2D { get; set; } = new RaylibCamera2D();
        public IImGui ImGui { get; set; }
        public RenderTexture2D RenderTexture2D { get; set; }
        public bool DebugDraw { get; set; } = false;
        public float DeltaTime => Raylib.GetFrameTime();
        
        public void DrawCircle(float x, float y, float r, bool fill = true)
        {
            if (fill)
                Raylib.DrawCircle((int)x, (int)y, r, Color.RED);
            else
                Raylib.DrawCircleLines((int)x, (int)y, r, Color.WHITE);
        }

        public void DrawRectangle(float x, float y, int w, int h, bool fill = true)
        {
            if (fill)
                Raylib.DrawRectangle((int)x - w / 2, (int)y - h / 2, w, h, Color.GREEN);
            else
                Raylib.DrawRectangleLines((int)x - w / 2, (int)y - h / 2, w, h, Color.WHITE);
        }

        public ISprite CreateSprite(string path)
        {
            if (_loadedSprites.ContainsKey(path))
                return _loadedSprites.GetValueOrDefault(path);

            var sprite = new RaylibSprite(path);
            _loadedSprites.Add(path, sprite);

            return sprite;
        }

        public ISprite CreateSpritesheet(string path, float tileWidth, float tileHeight)
        {
            if (_loadedSprites.ContainsKey(path))
                return _loadedSprites.GetValueOrDefault(path);
            
            var sprite = new RaylibSprite(path, tileWidth, tileHeight);
            _loadedSprites.Add(path, sprite);

            return sprite;
        }

        public void DrawSprite(ISprite sprite, float x, float y)
        {
            if (sprite is RaylibSprite raylibSprite)
                Raylib.DrawTexture(raylibSprite.Texture2D, (int) (x - sprite.Width / 2), (int) (y - sprite.Height / 2), Color.WHITE);

            if (DebugDraw)
                Raylib.DrawRectangleLines((int) (x - sprite.Width / 2), (int) (y - sprite.Height / 2), (int) sprite.Width, (int) sprite.Height, Color.RED);
        }

        public void DrawSprite(ISprite sprite, float x, float y, float tx, float ty, bool flipX = false, bool flipY = false)
        {
            float xMod = flipX ? -1 : 1;
            float yMod = flipY ? -1 : 1;
            if (sprite is RaylibSprite raylibSprite && raylibSprite.IsSpritesheet)
                Raylib.DrawTextureRec(raylibSprite.Texture2D, 
                    new Rectangle(raylibSprite.TileWidth * tx, raylibSprite.TileHeight * ty, raylibSprite.TileWidth * xMod, raylibSprite.TileHeight * yMod),
                    new Vector2(x - sprite.TileHeight / 2, y - sprite.TileWidth / 2), 
                    Color.WHITE);
            
            if (DebugDraw)
                Raylib.DrawRectangleLines((int) (x - sprite.TileWidth / 2), (int) (y - sprite.TileHeight / 2), (int) sprite.TileWidth, (int) sprite.TileHeight, Color.RED);
        }

        public void InitImGui()
        {
            rlImGui.Setup();
            ImGuiNET.ImGui.GetIO().ConfigFlags = ImGuiConfigFlags.DockingEnable;
        }

        public void BeginImGui()
        {
            rlImGui.Begin();
        }

        public void EndImGui()
        {
            rlImGui.End();
        }

        public void CleanupImGui()
        {
            rlImGui.Shutdown();
        }

        public void DrawGameImGui(int w, int h)
        {
            rlImGui.ImageRect(RenderTexture2D.texture, w, h, 
                new Rectangle(0, 0, w, -h));
            //rlImGui.ImageRect(RenderTexture2D.texture, w, h, 
                //new Rectangle(RenderTexture2D.texture.width / 2 - w / 2, RenderTexture2D.texture.height / 2 - h / 2, w, -h));
        }

        /*private void IsInView(float x, float y)
        {
            ((RaylibCamera2D)Camera2D).Camera2D.
        }*/
    }
}