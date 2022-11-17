using System;
using System.Linq;
using System.Numerics;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;
using SignE.Core.ECS.Components.Physics;
using SignE.Core.Extensions;
using SignE.ExampleGame.ECS.Components;

namespace SignE.ExampleGame.ECS.Systems;

public class BounceSystem : GameSystem
{
    public override void UpdateSystem()
    {
        foreach (var entity in Entities)
        {
            var mover = entity.GetComponent<PhysicsMoverComponent>();
            var bounce = entity.GetComponent<BounceComponent>();
            if (bounce.Angle == 0.0)
            {
                var rand = new Random();
                bounce.Angle = (float) rand.NextDouble() * 360;
            }

            if (mover.VelX == 0.0 || mover.VelY == 0.0)
                bounce.Angle += 90;

            var vel = Vector2FromAngle(bounce.Angle);
            mover.VelX = vel.X * 600;
            mover.VelY = vel.Y * 600;
        }
    }

    private static Vector2 Vector2FromAngle(float a)
    {
        var aRad = (MathF.PI / 180) * a;
        return new Vector2(MathF.Cos(aRad), MathF.Sin(aRad));
    }

    public override void LateUpdateSystem()
    {
        
    }

    public override void DrawSystem()
    {
        
    }

    public override void GetEntities(World world)
    {
        Entities = world.Entities.WithComponent<BounceComponent>()
            .WithComponent<PhysicsMoverComponent>().ToList();
    }
}