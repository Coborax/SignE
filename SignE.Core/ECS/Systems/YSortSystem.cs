using System.Linq;
using SignE.Core.ECS.Components;
using SignE.Core.Extensions;

namespace SignE.Core.ECS.Systems
{
    public class YSortSystem : GameSystem
    {
        public override void UpdateSystem()
        {
            foreach (var entity in Entities)
            {
                var pos = entity.GetComponent<Position2DComponent>();
                var sprite = entity.GetComponent<SpriteComponent>();

                sprite.Depth = pos.Y;
            }
        }

        public override void DrawSystem()
        {
            
        }

        public override void GetEntities(World world)
        {
            Entities = world.Entities
                .WithComponent<YSortComponent>()
                .WithComponent<Position2DComponent>()
                .WithComponent<SpriteComponent>()
                .ToList();
        }
    }   
}