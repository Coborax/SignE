using Raylib_cs;
using SignE.Core.Graphics;

namespace SignE.Platforms.RayLib.Graphics
{
    public class RaylibSprite : ISprite
    {
        public string Path { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        
        public bool IsSpritesheet { get; set; }
        public float TileWidth { get; set; }
        public float TileHeight { get; set; }

        public Texture2D Texture2D { get; }
        
        public RaylibSprite(string path)
        {
            Texture2D = Raylib.LoadTexture(path);
            Width = Texture2D.width;
            Height = Texture2D.height;
        }
        
        public RaylibSprite(string path, float tileWidth, float tileHeight)
        {
            Texture2D = Raylib.LoadTexture(path);
            Width = Texture2D.width;
            Height = Texture2D.height;

            IsSpritesheet = true;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }
        
        ~RaylibSprite()
        {
            Raylib.UnloadTexture(Texture2D);
        }
    }
}