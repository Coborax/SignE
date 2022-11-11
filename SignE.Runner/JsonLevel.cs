using SignE.Core.ECS;
using SignE.Core.Levels;
using SignE.Runner.Models;

namespace SignE.Runner
{
    public class JsonLevel : Level
    {
        public override string Name { get; set; }
        public string File { get; set; }
        public LevelModel LevelModel { get; set; }

        public override void LoadLevel()
        {
            var world = new World();

            foreach (var entityModel in LevelModel.Entities)
            {
                var entity = new Entity(entityModel.Id);
                foreach (var component in entityModel.Components)
                {
                    entity.AddComponent(component);
                }
                world.AddEntity(entity);
            }

            foreach (var gameSystem in LevelModel.GameSystems)
            {
                world.RegisterSystem(gameSystem);
            }

            World = world;
        }
    }
}