using System;

namespace SignE.Core.Graphics
{
    public interface ISprite
    {
        public string Path { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }   
}