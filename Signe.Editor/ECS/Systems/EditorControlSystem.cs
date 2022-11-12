using System;
using SignE.Core.ECS;
using SignE.Core.Input;

namespace Signe.Editor.ECS.Systems
{
    public class EditorControlSystem : GameSystem
    {
        public override void UpdateSystem()
        {
            var camera = SignE.Core.SignE.Graphics.Camera2D;
            var dm = SignE.Core.SignE.Input.GetMouseWheelMove();
            camera.Zoom += dm;
            
            if (!SignE.Core.SignE.Input.IsMouseButtonDown(MouseButton.MIDDLE_BUTTON)) return;
            var dx = SignE.Core.SignE.Input.GetMouseDeltaX();
            var dy = SignE.Core.SignE.Input.GetMouseDeltaY();

            if (dx == 0 && dy == 0) return;
            
            camera.X -= dx / camera.Zoom;
            camera.Y -= dy / camera.Zoom;
        }

        public override void LateUpdateSystem()
        {
            
        }

        public override void DrawSystem()
        {
            
        }

        public override void GetEntities(World world)
        {
            
        }
    }
}