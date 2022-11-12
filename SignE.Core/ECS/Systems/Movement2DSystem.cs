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
                var movement = entity.GetComponent<Movement2DComponent>();

                movement.VelX = 0;
                movement.VelY = 0;
                
                if (SignE.Input.IsKeyDown(Key.W)) 
                    movement.VelY  = -(movement.Speed * SignE.Graphics.DeltaTime);
                
                if (SignE.Input.IsKeyDown(Key.S)) 
                    movement.VelY  = (movement.Speed * SignE.Graphics.DeltaTime);
                
                if (SignE.Input.IsKeyDown(Key.D))
                    movement.VelX  = (movement.Speed * SignE.Graphics.DeltaTime);
                
                if (SignE.Input.IsKeyDown(Key.A))
                    movement.VelX  = -(movement.Speed * SignE.Graphics.DeltaTime);

                pos.X += movement.VelX;
                pos.Y += movement.VelY;
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