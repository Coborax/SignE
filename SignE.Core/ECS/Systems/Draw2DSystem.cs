using System.Collections.Generic;
using System.Linq;
using SignE.Core.ECS.Components;
using SignE.Core.Extensions;
using SignE.Core.Input;

namespace SignE.Core.ECS.Systems
{
    public class Draw2DSystem : GameSystem
    {
        
        private List<Entity> _rectangles;
        private List<Entity> _circles;
        private List<Entity> _sprites;
        
        public override void UpdateSystem()
        {
            
        }

        public override void DrawSystem()
        {
            DrawCircles();
            DrawSquares();
            DrawSprites();
        }

        public override void GetEntities(World world)
        {
            _rectangles = world.Entities
                .WithComponent<Position2DComponent>()
                .WithComponent<RectangleComponent>()
                .ToList();
            
            _circles = world.Entities
                .WithComponent<Position2DComponent>()
                .WithComponent<CircleComponent>()
                .ToList();
            
            _sprites = world.Entities
                .WithComponent<Position2DComponent>()
                .WithComponent<SpriteComponent>()
                .ToList();
        }

        private void DrawSquares()
        {
            foreach (var entity in _rectangles)
            {
                var pos = entity.GetComponent<Position2DComponent>();
                var square = entity.GetComponent<RectangleComponent>();
                SignE.Graphics.DrawRectangle(pos.X, pos.Y, square.Width, square.Height);
            }
        }

        private void DrawCircles()
        {
            foreach (var entity in _circles)
            {
                var pos = entity.GetComponent<Position2DComponent>();
                var r = entity.GetComponent<CircleComponent>().Radius;
                SignE.Graphics.DrawCircle(pos.X, pos.Y, r);
            }
        }

        private void DrawSprites()
        {
            _sprites.Sort(CompareBySpriteDepth);
            foreach (var entity in _sprites)
            {
                var pos = entity.GetComponent<Position2DComponent>();
                var sprite = entity.GetComponent<SpriteComponent>();

                if (sprite.Sprite.IsSpritesheet)
                    SignE.Graphics.DrawSprite(sprite.Sprite, pos.X, pos.Y, sprite.TileX, sprite.TileY, sprite.FlipX, sprite.FlipY);
                else 
                    SignE.Graphics.DrawSprite(sprite.Sprite, pos.X, pos.Y);
            }
        }

        private static int CompareBySpriteDepth(Entity a, Entity b)
        {
            var aSprite = a.GetComponent<SpriteComponent>();
            var bSprite = b.GetComponent<SpriteComponent>();

            return aSprite.Depth.CompareTo(bSprite.Depth);
        }
    }   
}