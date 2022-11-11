using System.Collections.Generic;
using SignE.Core.Graphics;

namespace SignE.Core.ECS.Components
{
    public class Animator2DComponent : IComponent
    {
        public float Timer { get; set; }
        public AnimationFrame CurrentFrame { get; set; }
        public Animation CurrentAnimation { get; set; }
        public List<Animation> Animations { get; set; }
        
        public void InitComponent()
        {
            
        }
    }

    public class Animation
    {
        public List<AnimationFrame> Frames { get; set; }
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