using System.Collections.Generic;
using System.Linq;

namespace SignE.Core.Levels
{
    public class LevelManager
    {
        public Level CurrentLevel { get; set; }
        private Dictionary<string, Level> _levels = new Dictionary<string, Level>();

        public void AddLevel(Level level)
        {
            _levels.Add(level.Name, level);
        }
        
        public void LoadLevel(string name)
        {
            CurrentLevel?.UnloadLevel();
            CurrentLevel = _levels.GetValueOrDefault(name);
            CurrentLevel?.LoadLevel();
        }

        public List<Level> GetLevelList()
        {
            return _levels.Values.ToList();
        }
        
    }
}