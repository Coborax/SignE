namespace SignE.Core.ECS.Components
{
    public class CircleComponent : IComponent
    {
        public float Radius { get; set; }

        public CircleComponent()
        {
            Radius = 10;
        }
        
        public CircleComponent(float r)
        {
            Radius = r;
        }

        public void InitComponent()
        {
        }
    }
}