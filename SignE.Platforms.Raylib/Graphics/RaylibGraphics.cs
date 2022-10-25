using Raylib_cs;
using SignE.Core.Graphics;

namespace SignE.Platforms.RayLib.Graphics
{
    public class RaylibGraphics : IGraphics
    {
        public void DrawCircle(float x, float y, float r)
        {
            Raylib.DrawCircle((int)x, (int)y, r, Color.RED);
        }

        public void DrawRectangle(float x, float y, int w, int h)
        {
            Raylib.DrawRectangle((int)x - w / 2, (int)y - h / 2, w, h, Color.RED);
        }
    }
}