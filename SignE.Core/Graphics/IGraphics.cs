using System.Runtime.InteropServices.ComTypes;

namespace SignE.Core.Graphics
{
    public enum Alignment
    {
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        Center,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }
    
    public interface IGraphics
    {
        public ICamera2D Camera2D { get; set; }
        public IImGui ImGui { get; set; }
        public bool DebugDraw { get; set; }
        public float DeltaTime { get; }
        
        // Primitive Shapes
        void DrawCircle(float x, float y, float r, bool fill = true);
        void DrawRectangle(float x, float y, int w, int h, bool fill = true, Alignment alignment = Alignment.Center);

        void Draw2DGrid();

        // Text
        void DrawText(float x, float y, int size, string str);
        
        // Sprites
        ISprite CreateSprite(string path);
        ISprite CreateSpritesheet(string path, float tileWidth, float tileHeight);
        void DrawSprite(ISprite sprite, float x, float y);
        void DrawSprite(ISprite sprite, float x, float y, float tx, float ty, bool flipX = false, bool flipY = false);
        
        //ImGui
        void InitImGui();
        void BeginImGui();
        void EndImGui();
        void CleanupImGui();
        void DrawGameImGui(int w, int h);
    }
}