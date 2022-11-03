using Newtonsoft.Json;
using SignE.Core.Graphics;

namespace SignE.Core.ECS.Components
{
    public class SpriteComponent : IComponent
    {
        [JsonIgnore]
        public ISprite Sprite { get; private set; }
        public string Path { get; set; }
        
        public float Depth { get; set; }

        public float TileX { get; set; }
        public float TileY { get; set; }
        public float TileW { get; set; }
        public float TileH { get; set; }

        public bool FlipX { get; set; } = false;
        public bool FlipY { get; set; } = false;
        public bool IsSpritesheet { get; set; } = false;

        public SpriteComponent()
        {
            
        }
        
        public SpriteComponent(string path, float depth = 0.0f)
        {
            Path = path;
            Depth = depth;
        }
        
        public SpriteComponent(string path, float tileW, float tileH, float depth = 0.0f)
        {
            Path = path;
            TileW = tileW;
            TileH = tileH;
            Depth = depth;
        }
        
        public SpriteComponent(string path, float tileW, float tileH, bool flipX, bool flipY, float depth = 0.0f)
        {
            Path = path;
            TileW = tileW;
            TileH = tileH;
            Depth = depth;
            FlipX = flipX;
            FlipY = flipY;
        }

        public void InitComponent()
        {
            if (TileW == 0.0f && TileH == 0.0f)
                Sprite = SignE.Graphics.CreateSprite(Path);
            else
                Sprite = SignE.Graphics.CreateSpritesheet(Path, TileW, TileH);
        }
    }   
}