namespace SignE.Core.ECS.Components
{
    public class Movement2DComponent : IComponent
    {
        public float Speed { get; set; } = 50.0f;
        public float VelX { get; set; }
        public float VelY { get; set; }
        public void InitComponent()
        {
            
        }
    }
}