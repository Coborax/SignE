using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        public bool QuadtreeDebugDraw { get; set; } = false;
        
        private List<Entity> _simpleMovers;
        private Quadtree _quadtree = new Quadtree(0, -1920/2, -1080/2, 1920, 1080);
        
        // Used for debug drawing quadtree
        private bool _added = false;
        private List<Entity> _closeEntities = new List<Entity>();

        public override void UpdateSystem()
        {
            
        }

        public override void LateUpdateSystem()
        {
            if (UseQuadtree)
            {
                //_quadtree.Clear();
                if (!_added)
                {
                    foreach (var entity in Entities)
                    {
                        _quadtree.Insert(entity);
                    }

                    _added = true;
                }

                //var closeEntities = new List<QuadtreeRect>();
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
                    var closeEntities = _quadtree.Retrieve(new Position2DComponent{X = posX.X, Y = posY.Y}, aabb);
                    foreach (var other in closeEntities.Where(other => entity != other))
                    {
                        UpdateCollisionVelocity(posX, posY, mover, aabb, other);
                    }

                    if (QuadtreeDebugDraw && entity.HasComponent<Movement2DComponent>())
                        _closeEntities = closeEntities;
                    
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
            if (UseQuadtree && QuadtreeDebugDraw)
            {
                _quadtree.DebugDraw();
                foreach (var closeEntity in _closeEntities)
                {
                    var pos = closeEntity.GetComponent<Position2DComponent>();
                    var aabb = closeEntity.GetComponent<AABBComponent>();
                    SignE.Graphics.DrawRectangle(pos.X, pos.Y, aabb.Width, aabb.Height);
                }
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
    public int MaxLevel { get; set; } = 5;

    public Position2DComponent Position { get; set; }
    public AABBComponent BoundingBox { get; set; }

    private List<Entity> _objects = new List<Entity>();
    private Quadtree[] _children = new Quadtree[4];

    private int _level;

    public Quadtree(int level, float x, float y, int w, int h)
    {
        _level = level;
        Position = new Position2DComponent(x, y);
        BoundingBox = new AABBComponent(w, h);
    }

    public void Insert(Entity entity)
    {
        var position = entity.GetComponent<Position2DComponent>();
        var boundingBox = entity.GetComponent<AABBComponent>();

        if (!IsInBoundingBox(position, boundingBox)) return;
        _objects.Add(entity);
        
        if (_children[0] == null && _objects.Count > MaxObjects && _level <= MaxLevel)
            Split();

        if (_children[0] == null) return;
        foreach (var child in _children)
        {
            child.Insert(entity);
        }
    }

    private void Split()
    {
        _children[0] = new Quadtree(_level + 1, Position.X, Position.Y, BoundingBox.Width / 2, BoundingBox.Height / 2);
        _children[1] = new Quadtree(_level + 1, Position.X + BoundingBox.Width /2, Position.Y, BoundingBox.Width / 2, BoundingBox.Height / 2);
        _children[2] = new Quadtree(_level + 1, Position.X + BoundingBox.Width /2, Position.Y + BoundingBox.Height / 2, BoundingBox.Width / 2, BoundingBox.Height / 2);
        _children[3] = new Quadtree(_level + 1, Position.X, Position.Y + BoundingBox.Height /2, BoundingBox.Width / 2, BoundingBox.Height / 2);
        
        foreach (var obj in _objects)
        {
            foreach (var child in _children)
            {
                child.Insert(obj);
            }
        }
        _objects.Clear();
    }

    public List<Entity> Retrieve(Position2DComponent position, AABBComponent boundingBox)
    {
        var result = new List<Entity>();
        if (!IsInBoundingBox(position, boundingBox)) return result;

        if (_children[0] == null)
        {
            result.AddRange(_objects);
        }
        else
        {
            foreach (var child in _children)
                result.AddRange(child.Retrieve(position, boundingBox));
        }

        return result;
    }

    private bool IsInBoundingBox(Position2DComponent position, AABBComponent boundingBox)
    {
        var aIsToTheRightOfB = position.X - boundingBox.Width / 2 > Position.X + BoundingBox.Width;
        var aIsToTheLeftOfB = position.X + boundingBox.Width / 2 < Position.X;
        var aIsAboveB = position.Y + boundingBox.Height / 2 < Position.Y;
        var aIsBelowB = position.Y - boundingBox.Height / 2 > Position.Y + BoundingBox.Height;

        return !(aIsToTheRightOfB
                 || aIsToTheLeftOfB
                 || aIsAboveB
                 || aIsBelowB);
    }

    /*private bool IsPointInBoundingBox(Position2DComponent point)
    {
        var bbTopLeft = Position;
        var bbBottomRight = new Position2DComponent(Position.X + BoundingBox.Width, Position.Y + BoundingBox.Height);

        return point.X >= bbTopLeft.X && point.Y >= bbTopLeft.Y && point.X <= bbBottomRight.X &&
               point.Y <= bbBottomRight.Y;
    }*/
    
    public void DebugDraw()
    {
        SignE.Core.SignE.Graphics.DrawRectangle(Position.X, Position.Y, BoundingBox.Width, BoundingBox.Height, false, Alignment.TopLeft);
        if (_children[0] == null)
            return;
        
        foreach (var child in _children)
        {
            child.DebugDraw();
        }
    }
}