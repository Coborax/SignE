using SignE.Core.ECS;
using SignE.Core.ECS.Components;

namespace SignE.ExampleGame.ECS.Components
{
    public class SmoothFollowComponent : IComponent
    {
        public Position2DComponent Target { get; set; }
        public float Smooth { get; set; }

        public SmoothFollowComponent(Position2DComponent target, float smooth)
        {
            Target = target;
            Smooth = smooth;
        }

        public void InitComponent()
        {
            
        }
    }
}