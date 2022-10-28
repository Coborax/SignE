using System.Runtime.InteropServices.ComTypes;

namespace SignE.Core.Graphics
{
    public interface IGraphics
    {
        public ICamera2D Camera2D { get; set; }
        public bool DebugDraw { get; set; }
        public float DeltaTime { get; }
        
        // Primitive Shapes
        void DrawCircle(float x, float y, float r);
        void DrawRectangle(float x, float y, int w, int h);
        
        // Sprites
        ISprite CreateSprite(string path);
        ISprite CreateSpritesheet(string path, float tileWidth, float tileHeight);
        void DrawSprite(ISprite sprite, float x, float y);
        void DrawSprite(ISprite sprite, float x, float y, float tx, float ty, bool flipX = false, bool flipY = false);
    }
}