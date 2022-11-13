using System;
using System.Collections.Generic;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;
using SignE.Core.ECS.Components.Physics;

namespace SignE.ExampleGame.ECS.Systems;

public class QuadtreeTestSpawnerSystem : GameSystem
{
    private List<Entity> _entities = new List<Entity>();
    private World _world;

    public override void UpdateSystem()
    {
        if (_entities.Count == 0)
        {
            var rand = new Random();
            
            for (var i = 0; i < 200; i++)
            {
                var entity = new Entity(Guid.NewGuid());
                entity.AddComponent(new Position2DComponent(rand.Next(-1920/2, 1920/2), rand.Next(-1080/2, 1080/2)));
                entity.AddComponent(new AABBComponent(rand.Next(10, 50), rand.Next(10, 50)));
                
                _entities.Add(entity);
                _world.AddEntity(entity);
            }
        }
    }

    public override void LateUpdateSystem()
    {
        
    }

    public override void DrawSystem()
    {
        
    }

    public override void GetEntities(World world)
    {
        _world = world;
    }
}