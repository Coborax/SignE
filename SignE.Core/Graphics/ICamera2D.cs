using SignE.Core.ECS;

namespace SignE.Core.Graphics
{
    public interface ICamera2D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Zoom { get; set; }

        public float Width { get; }
        public float Height { get; }
        
        public Entity Target { get; set; }
    }   
}