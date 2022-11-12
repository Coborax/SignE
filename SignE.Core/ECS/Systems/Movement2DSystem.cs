using System.Linq;
using SignE.Core.ECS.Components;
using SignE.Core.ECS.Components.Physics;
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
                var mover = entity.GetComponent<PhysicsMoverComponent>();
                var movement = entity.GetComponent<Movement2DComponent>();

                mover.VelX = 0;

                if (SignE.Input.IsKeyDown(Key.D))
                    mover.VelX = movement.Speed;

                if (SignE.Input.IsKeyDown(Key.A))
                    mover.VelX = -movement.Speed;
                
                if (SignE.Input.IsKeyPressed(Key.SPACE))
                    mover.VelY -= movement.JumpSpeed;
            }
        }

        public override void LateUpdateSystem()
        {
            
        }

        public override void DrawSystem()
        {
            
        }

        public override void GetEntities(World world)
        {
            Entities = world.Entities
                .WithComponent<PhysicsMoverComponent>()
                .WithComponent<Movement2DComponent>()
                .ToList();
        }
    }
}