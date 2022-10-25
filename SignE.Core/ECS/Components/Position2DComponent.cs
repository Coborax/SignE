namespace SignE.Core.ECS.Components
{
    public class Position2DComponent
    {
        public Position2DComponent(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        
        public float X { get; set; }
        public float Y { get; set; }
    }
}