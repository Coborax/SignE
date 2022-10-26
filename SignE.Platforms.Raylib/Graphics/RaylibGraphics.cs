using System.Collections.Generic;
using Raylib_cs;
using SignE.Core.Graphics;

namespace SignE.Platforms.RayLib.Graphics
{
    public class RaylibGraphics : IGraphics
    {
        private Dictionary<string, ISprite> _loadedSprites = new Dictionary<string, ISprite>();

        public void DrawCircle(float x, float y, float r)
        {
            Raylib.DrawCircle((int)x, (int)y, r, Color.RED);
        }

        public void DrawRectangle(float x, float y, int w, int h)
        {
            Raylib.DrawRectangle((int)x - w / 2, (int)y - h / 2, w, h, Color.GREEN);
        }

        public ISprite CreateSprite(string path)
        {
            if (_loadedSprites.ContainsKey(path))
                return _loadedSprites.GetValueOrDefault(path);

            var sprite = new RaylibSprite(path);
            _loadedSprites.Add(path, sprite);

            return sprite;
        }
        
        public void DrawSprite(ISprite sprite, float x, float y)
        {
            if (sprite is RaylibSprite raylibSprite)
                Raylib.DrawTexture(raylibSprite.Texture2D, (int) (x - sprite.Width / 2), (int) (y - sprite.Height / 2), Color.WHITE);
        }
    }
}