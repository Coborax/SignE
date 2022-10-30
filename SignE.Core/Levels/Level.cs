using SignE.Core.ECS;

namespace SignE.Core.Levels
{
    public abstract class Level
    {
        public abstract string Name { get; set; }
        public World World { get; protected set; }
        public bool Paused { get; set; } = false;
        
        public abstract void LoadLevel();
        public void UnloadLevel()
        {
            World = null;
        }
    }
}