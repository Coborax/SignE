using Raylib_cs;
using SignE.Core.Graphics;

namespace SignE.Platforms.RayLib.Graphics
{
    public class RaylibSprite : ISprite
    {
        public string Path { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Texture2D Texture2D { get; }
        
        public RaylibSprite(string path)
        {
            Texture2D = Raylib.LoadTexture(path);
            Width = Texture2D.width;
            Height = Texture2D.height;
        }
        
        ~RaylibSprite()
        {
            Raylib.UnloadTexture(Texture2D);
        }
    }
}