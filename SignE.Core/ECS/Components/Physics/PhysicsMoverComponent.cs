namespace SignE.Core.ECS.Components.Physics
{
    public class PhysicsMoverComponent : IComponent
    {
        public float VelX { get; set; }
        public float VelY { get; set; }

        public float Gravity { get; set; } = 200.0f;
        
        public void InitComponent()
        {
            
        }
    }
}