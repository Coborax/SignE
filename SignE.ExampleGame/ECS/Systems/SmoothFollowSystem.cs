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

                //TODO: Make it easier to get entity with Id, or make component entities automatically replace with entities in world for direct use
                var target = Core.SignE.LevelManager.CurrentLevel.World.Entities.Find(e => e.Id == follow.Target.Id);
                if (target == null)
                    continue;
                
                var targetPos = target.GetComponent<Position2DComponent>();
                pos.X = Math.Lerp(pos.X, targetPos.X, follow.Smooth);
                pos.Y = Math.Lerp(pos.Y, targetPos.Y, follow.Smooth);
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