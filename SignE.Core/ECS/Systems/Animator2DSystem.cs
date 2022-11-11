using System;
using System.Linq;
using SignE.Core.ECS.Components;
using SignE.Core.Extensions;

namespace SignE.Core.ECS.Systems
{
    public class Animator2DSystem : GameSystem
    {
        public override void UpdateSystem()
        {
            foreach (var entity in Entities)
            {
                var sprite = entity.GetComponent<SpriteComponent>();
                var animator = entity.GetComponent<Animator2DComponent>();

                if (animator.CurrentAnimation == null)
                {
                    animator.CurrentAnimation = animator.Animations[1];
                    animator.CurrentFrame = animator.CurrentAnimation.Frames[0];
                }
                
                if (animator.CurrentFrame == null || animator.CurrentAnimation == null)
                    continue;
                
                animator.Timer += SignE.Graphics.DeltaTime;
                if (animator.Timer >= animator.CurrentFrame.FrameTime)
                {
                    animator.Timer = 0.0f;
                    var idx = animator.CurrentAnimation.Frames.IndexOf(animator.CurrentFrame);
                    var newIdx = idx + 1 == animator.CurrentAnimation.Frames.Count ? 0 : idx + 1;

                    animator.CurrentFrame = animator.CurrentAnimation.Frames[newIdx];
                }
                
                sprite.TileX = animator.CurrentFrame.TileX;
                sprite.TileY = animator.CurrentFrame.TileY;
                sprite.FlipX = animator.CurrentFrame.FlipX;
                sprite.FlipY = animator.CurrentFrame.FlipY;
            }
        }

        public override void DrawSystem()
        {
            
        }

        public override void GetEntities(World world)
        {
            Entities = world.Entities
                .WithComponent<SpriteComponent>()
                .WithComponent<Animator2DComponent>().ToList();
        }
    }
}