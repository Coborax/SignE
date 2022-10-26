using SignE.Core.Graphics;

namespace SignE.Core.ECS.Components
{
    public class SpriteComponent : IComponent
    {
        public ISprite Sprite { get; }
        public float Depth { get; set; }

        public SpriteComponent(string path, float depth = 0.0f)
        {
            Sprite = SignE.Graphics.CreateSprite(path);
            Depth = depth;
        }
    }   
}