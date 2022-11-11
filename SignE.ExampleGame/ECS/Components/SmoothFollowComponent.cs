using SignE.Core.ECS;
using SignE.Core.ECS.Components;

namespace SignE.ExampleGame.ECS.Components
{
    public class SmoothFollowComponent : IComponent
    {
        public Entity Target { get; set; }
        public float Smooth { get; set; }
        public float NewPropertyHihi { get; set; }

        public void InitComponent()
        {
            
        }
    }
}