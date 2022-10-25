namespace SignE.Core.ECS.Components
{
    public class CircleComponent : IComponent
    {
        public float Radius { get; set; }

        public CircleComponent(float r)
        {
            Radius = r;
        }
    }
}