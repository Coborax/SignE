using System.Linq;
using SignE.Core.ECS.Components;
using SignE.Core.Extensions;

namespace SignE.Core.ECS.Systems
{
    public class Camera2DSystem : GameSystem
    {
        public override void UpdateSystem()
        {
            if (Entities.Count != 1) return;
            
            var pos = Entities[0].GetComponent<Position2DComponent>();
            
            SignE.Graphics.Camera2D.X = pos.X;
            SignE.Graphics.Camera2D.Y = pos.Y;
        }

        public override void DrawSystem()
        {
        
        }

        public override void GetEntities(World world)
        {
            Entities = world.Entities
                .WithComponent<Camera2DComponent>()
                .WithComponent<Position2DComponent>()
                .ToList();
        }
    }
}
