using System;
using System.Collections.Generic;
using System.Linq;
using SignE.Core.ECS.Components;
using SignE.Core.ECS.Components.Physics;
using SignE.Core.Extensions;

namespace SignE.Core.ECS.Systems.Physics
{
    public class SimplePhysicsSystem : GameSystem
    {

        private List<Entity> _simpleMovers;

        public override void UpdateSystem()
        {
            
        }

        public override void LateUpdateSystem()
        {
            foreach (var entity in _simpleMovers)
            {
                var mover = entity.GetComponent<PhysicsMoverComponent>();
                var entityPos = entity.GetComponent<Position2DComponent>();
                var aabb = entity.GetComponent<AABBComponent>();

                mover.VelY += mover.Gravity;
                
                var posX = new Position2DComponent
                {
                    X = entityPos.X + mover.VelX * SignE.Graphics.DeltaTime,
                    Y = entityPos.Y
                };
                
                var posY = new Position2DComponent
                {
                    X = entityPos.X,
                    Y = entityPos.Y + mover.VelY * SignE.Graphics.DeltaTime
                };   
                
                foreach (var other in Entities)
                {
                    if (entity == other)
                        continue;

                    var otherPos = other.GetComponent<Position2DComponent>();
                    var otherAabb = other.GetComponent<AABBComponent>();

                    if (CheckCollision(posX, aabb, otherPos, otherAabb))
                        mover.VelX = 0;

                    if (CheckCollision(posY, aabb, otherPos, otherAabb))
                        mover.VelY = 0;
                }

                entityPos.X += mover.VelX * SignE.Graphics.DeltaTime;
                entityPos.Y += mover.VelY * SignE.Graphics.DeltaTime;
            }
        }

        private static bool CheckCollision(Position2DComponent aPos, AABBComponent aAabb, Position2DComponent bPos, AABBComponent bAabb)
        {
            var aIsToTheRightOfB = aPos.X - aAabb.Width / 2 > bPos.X + bAabb.Width / 2;
            var aIsToTheLeftOfB = aPos.X + aAabb.Width / 2 < bPos.X - bAabb.Width / 2;
            var aIsAboveB = aPos.Y + aAabb.Height / 2 < bPos.Y - bAabb.Height / 2;
            var aIsBelowB = aPos.Y - aAabb.Height / 2 > bPos.Y + bAabb.Height / 2;

            return !(aIsToTheRightOfB
                     || aIsToTheLeftOfB
                     || aIsAboveB
                     || aIsBelowB);
        }

        public override void DrawSystem()
        {
            foreach (var entity in Entities)
            {
                var pos = entity.GetComponent<Position2DComponent>();
                var aabb = entity.GetComponent<AABBComponent>();

                SignE.Graphics.DrawRectangle(pos.X, pos.Y, aabb.Width, aabb.Height, false);
            }
        }

        public override void GetEntities(World world)
        {
            _simpleMovers = world.Entities.WithComponent<Position2DComponent>().WithComponent<AABBComponent>()
                .WithComponent<PhysicsMoverComponent>().ToList();
            Entities = world.Entities.WithComponent<Position2DComponent>().WithComponent<AABBComponent>().ToList();
        }
    }
}