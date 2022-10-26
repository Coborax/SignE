namespace SignE.Core.ECS.Components
{
    public class Position2DComponent : IComponent
    {
        public Position2DComponent(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public float Y { get; set; }
        public float X { get; set; }
    }
}