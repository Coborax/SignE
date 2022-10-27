using System.Linq;
using SignE.Core.ECS.Components;
using SignE.Core.Extensions;

namespace SignE.Core.ECS.Systems
{
    public class Camera2DSystem : IGameSystem
    {
        public void UpdateSystem(World world)
        {
            var entities = world.Entities
                .WithComponent<Camera2DComponent>()
                .WithComponent<Position2DComponent>()
                .ToList();

            if (entities.Count != 1) return;
            
            var pos = entities[0].GetComponent<Position2DComponent>();
            
            SignE.Graphics.Camera2D.X = pos.X;
            SignE.Graphics.Camera2D.Y = pos.Y;
        }

        public void DrawSystem(World world)
        {
        
        }
    }
}
