namespace SignE.Core.ECS.Components
{
    public class RectangleComponent : IComponent
    {
        public RectangleComponent(int w, int h)
        {
            Width = w;
            Height = h;
        }
        
        public int Width { get; set; }
        public int Height { get; set; }
    }
}