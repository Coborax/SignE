using System.Linq;
using SignE.Core.ECS.Components;
using SignE.Core.Extensions;

namespace SignE.Core.ECS.Systems
{
    public class DrawGameSystem : IGameSystem
    {
        public void UpdateSystem(World world)
        {
            
        }

        public void DrawSystem(World world)
        {
            DrawCircles(world);
            DrawSquares(world);
        }

        private void DrawSquares(World world)
        {
            var entities = world.Entities
                .WithComponent<Position2DComponent>()
                .WithComponent<RectangleComponent>()
                .ToList();
            
            foreach (var entity in entities)
            {
                var pos = entity.GetComponent<Position2DComponent>();
                var square = entity.GetComponent<RectangleComponent>();
                SignE.Graphics.DrawRectangle(pos.X, pos.Y, square.Width, square.Height);
            }
        }

        private void DrawCircles(World world)
        {
            var entities = world.Entities
                .WithComponent<Position2DComponent>()
                .WithComponent<CircleComponent>()
                .ToList();
            
            foreach (var entity in entities)
            {
                var pos = entity.GetComponent<Position2DComponent>();
                var r = entity.GetComponent<CircleComponent>().Radius;
                SignE.Graphics.DrawCircle(pos.X, pos.Y, r);
            }
        }
    }   
}