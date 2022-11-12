using System.Collections.Generic;
using SignE.Core.ECS.Systems;
using SignE.Core.Graphics;

namespace SignE.Core.ECS.Components
{
    public class Animator2DComponent : IComponent
    {
        public delegate void AnimationEndEventHandler(object sender, Animation animation);
        public event AnimationEndEventHandler AnimationEnd;
        
        public float Timer { get; set; }
        public AnimationFrame CurrentFrame { get; set; }
        public Animation CurrentAnimation { get; set; }
        public List<Animation> Animations { get; set; }
        
        public void InitComponent()
        {
            
        }

        public void ChangeAnimation(Animation animation)
        {
            if (CurrentAnimation == animation) return;
            CurrentAnimation = animation;
            CurrentFrame = animation.Frames[0];
        }

        public void InvokeAnimationEnd(Animator2DSystem animator2DSystem)
        {
            AnimationEnd?.Invoke(animator2DSystem, CurrentAnimation);
        }
    }

    public class Animation
    {
        public List<AnimationFrame> Frames { get; set; }
        public bool Loop { get; set; } = false;
    }

    public class AnimationFrame
    {
        public bool FlipX { get; set; } = false;
        public bool FlipY { get; set; } = false;
        public float TileX { get; set; }
        public float TileY { get; set; }
        public float FrameTime { get; set; } = 0.5f;
    }
}