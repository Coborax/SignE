using System.Linq;
using SignE.Core.ECS.Components;
using SignE.Core.Extensions;
using SignE.Core.Input;

namespace SignE.Core.ECS.Systems
{
    public class Movement2DSystem : GameSystem
    {
        public override void UpdateSystem()
        {
            foreach (var entity in Entities)
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

        public override void DrawSystem()
        {
            
        }

        public override void GetEntities(World world)
        {
            Entities = world.Entities
                .WithComponent<Position2DComponent>()
                .WithComponent<Movement2DComponent>()
                .ToList();
        }
    }
}