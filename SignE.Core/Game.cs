using System;
using System.Collections.Generic;
using SignE.Core.ECS;

namespace SignE.Core
{
    public abstract class Game : IDisposable
    {
        public bool RenderGameToTexture { get; set; } = false;
        
        protected World World;

        public void Run()
        {
            Loop();
            Dispose();
        }

        public virtual void Init(int w, int h, string title, World world)
        {
            World = world;
        }

        protected abstract void Loop();
        public abstract void Dispose();
    }
}