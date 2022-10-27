﻿using SignE.Core.Graphics;

namespace SignE.Core.ECS.Components
{
    public class SpriteComponent : IComponent
    {
        public ISprite Sprite { get; }
        public float Depth { get; set; }

        public float TileX { get; set; } = 0;
        public float TileY { get; set; } = 0;

        public SpriteComponent(string path, float depth = 0.0f)
        {
            Sprite = SignE.Graphics.CreateSprite(path);
            Depth = depth;
        }
        
        public SpriteComponent(string path, float tileW, float tileH, float depth = 0.0f)
        {
            Sprite = SignE.Graphics.CreateSpritesheet(path, tileW, tileH);
            Depth = depth;
        }
    }   
}