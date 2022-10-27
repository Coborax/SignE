using System.Linq;
using SignE.Core.ECS.Components;
using SignE.Core.Extensions;
using SignE.Core.Input;

namespace SignE.Core.ECS.Systems
{
    public class Movement2DSystem : IGameSystem
    {
        public void UpdateSystem(World world)
        {
            var entities = world.Entities
                .WithComponent<Position2DComponent>()
                .WithComponent<Movement2DComponent>()
                .ToList();

            foreach (var entity in entities)
            {
                var pos = entity.GetComponent<Position2DComponent>();
                var speed = entity.GetComponent<Movement2DComponent>().Speed;
                
                if (SignE.Input.IsKeyDown(Key.W))
                    pos.Y -= speed * SignE.Graphics.DeltaTime;
                
                if (SignE.Input.IsKeyDown(Key.S))
                    pos.Y += speed * SignE.Graphics.DeltaTime;
                
                if (SignE.Input.IsKeyDown(Key.D))
                    pos.X += speed * SignE.Graphics.DeltaTime;
                
                if (SignE.Input.IsKeyDown(Key.A))
                    pos.X -= speed * SignE.Graphics.DeltaTime;

            }
        }

        public void DrawSystem(World world)
        {
            
        }
    }
}