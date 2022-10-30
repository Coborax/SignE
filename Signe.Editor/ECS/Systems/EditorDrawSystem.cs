using ImGuiNET;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;

namespace Signe.Editor.ECS.Systems
{
    public class EditorDrawSystem : GameSystem
    {
        private Editor _editor = Editor.MainEditor;
        
        public override void UpdateSystem()
        {
            
        }

        public override void DrawSystem()
        {
            foreach (var entity in Entities)
            {
                if (entity.HasComponent<Position2DComponent>())
                {
                    var pos = entity.GetComponent<Position2DComponent>();
                    SignE.Core.SignE.Graphics.DrawText(10, 10, 32, entity.Id.ToString());
                }
            }
            
            DrawSelectedEntity();
        }

        private void DrawSelectedEntity()
        {
            var selectedEntity = _editor.SelectedEntity;
            if (_editor.SelectedEntity == null || !_editor.SelectedEntity.HasComponent<Position2DComponent>()) return;

            var pos = selectedEntity.GetComponent<Position2DComponent>();
            if (selectedEntity.HasComponent<SpriteComponent>())
            {
                var sprite = _editor.SelectedEntity.GetComponent<SpriteComponent>();
                SignE.Core.SignE.Graphics.DrawRectangle(pos.X, pos.Y, (int)sprite.Sprite.Width + 2, (int)sprite.Sprite.Height + 2, false);
            }
            else if (selectedEntity.HasComponent<RectangleComponent>())
            {
                var rectangle = _editor.SelectedEntity.GetComponent<RectangleComponent>();
                SignE.Core.SignE.Graphics.DrawRectangle(pos.X, pos.Y, rectangle.Width + 2, rectangle.Height + 2, false);
            }
            else if (selectedEntity.HasComponent<CircleComponent>())
            {
                var circle = _editor.SelectedEntity.GetComponent<CircleComponent>();
                SignE.Core.SignE.Graphics.DrawCircle(pos.X, pos.Y, circle.Radius + 2, false);
            }
            else
            {
                SignE.Core.SignE.Graphics.DrawCircle(pos.X, pos.Y, 2.0f);
            }
        }

        public override void GetEntities(World world)
        {
            Entities = world.Entities;
        }
    }
}