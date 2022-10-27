using System;

namespace SignE.Core.Graphics
{
    public interface ISprite
    {
        public string Path { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public bool IsSpritesheet { get; set; }
        public float TileWidth { get; set; }
        public float TileHeight { get; set; }
    }   
}