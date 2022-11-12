namespace SignE.Core.ECS.Components
{
    public class Movement2DComponent : IComponent
    {
        public float Speed { get; set; } = 50.0f;
        public float JumpSpeed { get; set; } = 5.0f;

        public void InitComponent()
        {
            
        }
    }
}