namespace SignE.Core.ECS.Components.Physics
{
    public class AABBComponent : IComponent
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public AABBComponent()
        {
            
        }
        
        public AABBComponent(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void InitComponent()
        {
            
        }
    }
}