using System;
using System.Linq;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;
using SignE.Core.ECS.Components.Physics;
using SignE.Core.ECS.Systems;
using SignE.Core.Extensions;
using SignE.Core.Input;
using SignE.ExampleGame.ECS.Components;

namespace SignE.ExampleGame.ECS.Systems;

public class PlayerSystem : GameSystem
{
    public override void UpdateSystem()
    {
        foreach (var entity in Entities)
        {
            var player = entity.GetComponent<PlayerComponent>();
            var movement = entity.GetComponent<PhysicsMoverComponent>();
            var sprite = entity.GetComponent<SpriteComponent>();
            var animator = entity.GetComponent<Animator2DComponent>();

            sprite.FlipX = movement.VelX switch
            {
                < 0 => true,
                > 0 => false,
                _ => sprite.FlipX
            };

            switch (player.PlayerState)
            {
                case PlayerState.Idle:
                    UpdateIdle(animator);
                    UpdateIdleRunning(movement, player);
                    break;
                case PlayerState.Running:
                    UpdateRunning(player, animator);
                    UpdateIdleRunning(movement, player);
                    break;
                case PlayerState.Attacking:
                    UpdateAttacking(player, animator);
                    break;
                case PlayerState.Dead:
                    break;
            }
            
        }
    }

    public override void LateUpdateSystem()
    {
        
    }

    private void UpdateIdleRunning(PhysicsMoverComponent movement, PlayerComponent player)
    {
        if (movement.VelX != 0 || movement.VelY != 0)
            player.PlayerState = PlayerState.Running;
        else
            player.PlayerState = PlayerState.Idle;
        
        if (Core.SignE.Input.IsKeyPressed(Key.Z))
            player.PlayerState = PlayerState.Attacking;
    }
    
    private void UpdateIdle(Animator2DComponent animator)
    {
        animator.ChangeAnimation(animator.Animations[0]);
    }
    
    private void UpdateRunning(PlayerComponent player, Animator2DComponent animator)
    {
        animator.ChangeAnimation(animator.Animations[1]);
    }

    private void UpdateAttacking(PlayerComponent player, Animator2DComponent animator)
    {
        animator.ChangeAnimation(animator.Animations[2]);
        animator.AnimationEnd += (sender, animation) =>
        {
            player.PlayerState = PlayerState.Idle;
        };
    }

    public override void DrawSystem()
    {
        
    }

    public override void GetEntities(World world)
    {
        Entities = world.Entities
            .WithComponent<SpriteComponent>()
            .WithComponent<PhysicsMoverComponent>()
            .WithComponent<Animator2DComponent>()
            .WithComponent<PlayerComponent>().ToList();
    }
}