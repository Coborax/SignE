using System;
using System.Collections.Generic;
using System.Linq;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;
using SignE.Core.ECS.Components.Physics;
using SignE.Core.Extensions;
using SignE.Core.Graphics;

namespace SignE.Core.ECS.Systems.Physics
{
    public class SimplePhysicsSystem : GameSystem
    {

        public bool UseQuadtree { get; set; } = false;
        
        private List<Entity> _simpleMovers;

        private Quadtree _quadtree =
            new Quadtree(0, new QuadtreeRect(new Position2DComponent(-1920/2, -1080/2), new AABBComponent(1920, 1080)));

        public override void UpdateSystem()
        {
            
        }

        public override void LateUpdateSystem()
        {
            if (UseQuadtree)
            {
                _quadtree.Clear();
                foreach (var entity in Entities)
                {
                    var pos = entity.GetComponent<Position2DComponent>();
                    var aabb = entity.GetComponent<AABBComponent>();
                    _quadtree.Insert(new QuadtreeRect(pos, aabb, entity));
                }

                var closeEntities = new List<QuadtreeRect>();
                foreach (var entity in _simpleMovers)
                {
                    var mover = entity.GetComponent<PhysicsMoverComponent>();
                    var entityPos = entity.GetComponent<Position2DComponent>();
                    var aabb = entity.GetComponent<AABBComponent>();
                    
                    mover.VelY += mover.Gravity;
                
                    var posX = new Position2DComponent
                    {
                        X = entityPos.X + mover.VelX * SignE.Graphics.DeltaTime,
                        Y = entityPos.Y
                    };
                
                    var posY = new Position2DComponent
                    {
                        X = entityPos.X,
                        Y = entityPos.Y + mover.VelY * SignE.Graphics.DeltaTime
                    };   
                    
                    // Get close entities where our updated position would be
                    closeEntities.Clear();
                    _quadtree.Retrieve(ref closeEntities, new QuadtreeRect(new Position2DComponent{X = posX.X, Y = posY.Y}, aabb, entity));
                    foreach (var other in closeEntities)
                    {
                        if (entity == other.Entity)
                            continue;

                        other.IsClose = true;
                        UpdateCollisionVelocity(posX, posY, mover, aabb, other.Entity);
                    }
                    
                    entityPos.X += mover.VelX * SignE.Graphics.DeltaTime;
                    entityPos.Y += mover.VelY * SignE.Graphics.DeltaTime;
                }
            }
            else
            {
                foreach (var entity in _simpleMovers)
                {
                    var mover = entity.GetComponent<PhysicsMoverComponent>();
                    var entityPos = entity.GetComponent<Position2DComponent>();
                    var aabb = entity.GetComponent<AABBComponent>();
                    
                    mover.VelY += mover.Gravity;
                
                    var posX = new Position2DComponent
                    {
                        X = entityPos.X + mover.VelX * SignE.Graphics.DeltaTime,
                        Y = entityPos.Y
                    };
                
                    var posY = new Position2DComponent
                    {
                        X = entityPos.X,
                        Y = entityPos.Y + mover.VelY * SignE.Graphics.DeltaTime
                    };   
                    
                    foreach (var other in Entities)
                    {
                        if (entity == other)
                            continue;

                        UpdateCollisionVelocity(posX, posY, mover, aabb, other);
                    }
                    
                    entityPos.X += mover.VelX * SignE.Graphics.DeltaTime;
                    entityPos.Y += mover.VelY * SignE.Graphics.DeltaTime;
                }
            }
        }

        private void UpdateCollisionVelocity(Position2DComponent posX, Position2DComponent posY, PhysicsMoverComponent mover, AABBComponent aabb, Entity other)
        {
            var otherPos = other.GetComponent<Position2DComponent>();
            var otherAabb = other.GetComponent<AABBComponent>();

            if (CheckCollision(posX, aabb, otherPos, otherAabb))
                mover.VelX = 0;

            if (CheckCollision(posY, aabb, otherPos, otherAabb))
                mover.VelY = 0;
        }

        private static bool CheckCollision(Position2DComponent aPos, AABBComponent aAabb, Position2DComponent bPos, AABBComponent bAabb)
        {
            var aIsToTheRightOfB = aPos.X - aAabb.Width / 2 > bPos.X + bAabb.Width / 2;
            var aIsToTheLeftOfB = aPos.X + aAabb.Width / 2 < bPos.X - bAabb.Width / 2;
            var aIsAboveB = aPos.Y + aAabb.Height / 2 < bPos.Y - bAabb.Height / 2;
            var aIsBelowB = aPos.Y - aAabb.Height / 2 > bPos.Y + bAabb.Height / 2;

            return !(aIsToTheRightOfB
                     || aIsToTheLeftOfB
                     || aIsAboveB
                     || aIsBelowB);
        }

        public override void DrawSystem()
        {
            if (UseQuadtree)
            {
                Quadtree.DebugDraw(_quadtree);
            }
            
            foreach (var entity in Entities)
            {
                var pos = entity.GetComponent<Position2DComponent>();
                var aabb = entity.GetComponent<AABBComponent>();

                SignE.Graphics.DrawRectangle(pos.X, pos.Y, aabb.Width, aabb.Height, false);
            }
            
        }

        public override void GetEntities(World world)
        {
            _simpleMovers = world.Entities.WithComponent<Position2DComponent>().WithComponent<AABBComponent>()
                .WithComponent<PhysicsMoverComponent>().ToList();
            Entities = world.Entities.WithComponent<Position2DComponent>().WithComponent<AABBComponent>().ToList();
        }
    }
}

public class Quadtree
{
    public int MaxObjects { get; set; } = 10;
    public int MaxLevels { get; set; } = 5;

    private int _level;
    private List<QuadtreeRect> _objects;
    private QuadtreeRect _bounds;
    private Quadtree[] _nodes;

    public Quadtree(int level, QuadtreeRect bounds)
    {
        _level = level;
        _objects = new List<QuadtreeRect>();
        _bounds = bounds;
        _nodes = new Quadtree[4];
    }

    public void Clear()
    {
        _objects.Clear();
        for (var i = 0; i < _nodes.Length; i++)
        {
            if (_nodes[i] == null) continue;
            _nodes[i].Clear();
            _nodes[i] = null;
        }
    }
    
    private void Split() {
        var subWidth = _bounds.Bounds.Width / 2;
        var subHeight = _bounds.Bounds.Height / 2;
        var x = (int)_bounds.Position.X;
        var y = (int)_bounds.Position.Y;
 
        _nodes[0] = new Quadtree(_level + 1, new QuadtreeRect(new Position2DComponent(x + subWidth, y), new AABBComponent(subWidth, subHeight)));
        _nodes[1] = new Quadtree(_level + 1, new QuadtreeRect(new Position2DComponent(x, y), new AABBComponent(subWidth, subHeight)));
        _nodes[2] = new Quadtree(_level + 1, new QuadtreeRect(new Position2DComponent(x, y + subHeight), new AABBComponent(subWidth, subHeight)));
        _nodes[3] = new Quadtree(_level + 1, new QuadtreeRect(new Position2DComponent(x + subWidth, y + subHeight),new AABBComponent(subWidth, subHeight)));
    }

    private int GetIndex(QuadtreeRect rect)
    {
        var index = -1;
        var verticalMid = _bounds.Position.X + (_bounds.Bounds.Width / 2);
        var horizontalMid = _bounds.Position.Y + (_bounds.Bounds.Height / 2);

        var topQuadrant = rect.Position.Y < horizontalMid && rect.Position.Y + rect.Bounds.Height < horizontalMid; // Object can completely fit within the top quadrants
        var bottomQuadrant = rect.Position.Y > horizontalMid; // Object can completely fit within the bottom quadrants
        
        // Object can completely fit within the left quadrants
        if (rect.Position.X < verticalMid && rect.Position.X + rect.Bounds.Width < verticalMid) {
            if (topQuadrant) {
                index = 1;
            }
            else if (bottomQuadrant) {
                index = 2;
            }
        }
        // Object can completely fit within the right quadrants
        else if (rect.Position.X > verticalMid) {
            if (topQuadrant) {
                index = 0;
            }
            else if (bottomQuadrant) {
                index = 3;
            }
        }
 
        return index;
    }
    
    public void Insert(QuadtreeRect rect) {
        if (_nodes[0] != null) {
            var index = GetIndex(rect);
 
            if (index != -1) {
                _nodes[index].Insert(rect);
                return;
            }
        }
 
        _objects.Add(rect);
 
        if (_objects.Count == MaxObjects && _level < MaxLevels) {
            if (_nodes[0] == null) { 
                Split();
            }

            var i = 0;
            while (i < _objects.Count) {
                var index = GetIndex(_objects[i]);
                if (index != -1) {
                    _nodes[index].Insert(_objects[i]);
                    _objects.RemoveAt(i);
                }
                else {
                    i++;
                }
            }
        }
    }
    
    public List<QuadtreeRect> Retrieve(ref List<QuadtreeRect> result, QuadtreeRect rect)
    {
        var index = GetIndex(rect);
        if (index != -1 && _nodes[0] != null) {
            _nodes[index].Retrieve(ref result, rect);
        }
 
        result.AddRange(_objects);
        return result;
    }

    public static void DebugDraw(Quadtree quadtree)
    {
        if (quadtree == null)
            return;
        
        var rect = quadtree._bounds;
        SignE.Core.SignE.Graphics.DrawRectangle(rect.Position.X, rect.Position.Y, rect.Bounds.Width, rect.Bounds.Height, false, Alignment.TopLeft);

        foreach (var obj in quadtree._objects)
        {
            if (obj.IsClose)
            {
                SignE.Core.SignE.Graphics.DrawRectangle(obj.Position.X, obj.Position.Y, obj.Bounds.Width, obj.Bounds.Height);
            }
        }
        
        foreach (var node in quadtree._nodes)
        {
            DebugDraw(node);
        }
    }
}

public class QuadtreeRect : IComparable {
    public Entity Entity { get; set; }
    public Position2DComponent Position { get; set; }
    public AABBComponent Bounds { get; set; }

    public bool IsClose { get; set; } = false;

    public QuadtreeRect(Position2DComponent position, AABBComponent bounds)
    {
        Position = position;
        Bounds = bounds;
    }
    
    public QuadtreeRect(Position2DComponent position, AABBComponent bounds, Entity entity)
    {
        Position = position;
        Bounds = bounds;
        Entity = entity;
    }

    public int CompareTo(object obj)
    {
        var other = (QuadtreeRect)obj;
        return other.Entity.CompareTo(Entity);
    }
}