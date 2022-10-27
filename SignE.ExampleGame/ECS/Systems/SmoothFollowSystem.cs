using System.Linq;
using SignE.Core;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;
using SignE.Core.Extensions;
using SignE.ExampleGame.ECS.Components;

namespace SignE.ExampleGame.ECS.Systems
{
    public class SmoothFollowSystem : GameSystem
    {
        public override void UpdateSystem()
        {
            foreach (var entity in Entities)
            {
                var follow = entity.GetComponent<SmoothFollowComponent>();
                var pos = entity.GetComponent<Position2DComponent>();

                pos.X = Math.Lerp(pos.X, follow.Target.X, follow.Smooth);
                pos.Y = Math.Lerp(pos.Y, follow.Target.Y, follow.Smooth);
            }
        }

        public override void DrawSystem()
        {
            
        }

        public override void GetEntities(World world)
        {
            Entities = world.Entities
                .WithComponent<SmoothFollowComponent>()
                .WithComponent<Position2DComponent>()
                .ToList();
        }
    }
}