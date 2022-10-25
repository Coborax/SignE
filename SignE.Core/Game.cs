using System;
using System.Collections.Generic;

namespace SignE.Core
{
    public abstract class Game : IDisposable
    {
        public void Run(int w, int h, string title)
        {
            Init(w, h, title);
            Loop();
            Dispose();
        }

        protected abstract void Init(int w, int h, string title);
        protected abstract void Loop();
        public abstract void Dispose();
    }
}