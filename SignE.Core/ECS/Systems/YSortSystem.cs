using SignE.Core.ECS.Components;
using SignE.Core.Extensions;

namespace SignE.Core.ECS.Systems
{
    public class YSortSystem : IGameSystem
    {
        public void UpdateSystem(World world)
        {
            var entities = world.Entities
                .WithComponent<YSortComponent>()
                .WithComponent<Position2DComponent>()
                .WithComponent<SpriteComponent>();

            foreach (var entity in entities)
            {
                var pos = entity.GetComponent<Position2DComponent>();
                var sprite = entity.GetComponent<SpriteComponent>();

                sprite.Depth = pos.Y;
            }
        }

        public void DrawSystem(World world)
        {
        
        }
    }   
}