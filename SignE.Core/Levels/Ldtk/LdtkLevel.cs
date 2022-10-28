using System;
using System.IO;
using System.Linq;
using ldtk;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;
using SignE.Core.ECS.Systems;

namespace SignE.Core.Levels.Ldtk
{
    public class LdtkLevel : Level
    {
        public override string Name { get; set; }

        private LdtkJson _ldtk;

        public LdtkLevel(string ldtkFilePath, string levelName)
        {
            Name = levelName;
            _ldtk = LdtkJson.FromJson(File.ReadAllText(ldtkFilePath));
        }
        
        public override void LoadLevel()
        {
            World = new ECS.World();
            
            var levelToLoad = _ldtk.Levels.FirstOrDefault(l => l.Identifier == Name);
            if (levelToLoad == null)
                return;


            float depth = levelToLoad.LayerInstances.Length;
            foreach (var layerInstance in levelToLoad.LayerInstances)
            {
                switch (layerInstance.Type)
                {
                    case "Tiles":
                        CreateTilemap(layerInstance.GridTiles, layerInstance.TilesetRelPath, layerInstance.GridSize, depth);
                        break;
                    case "IntGrid":
                        CreateTilemap(layerInstance.AutoLayerTiles, layerInstance.TilesetRelPath, layerInstance.GridSize, depth);
                        break;
                }

                depth -= 1.0f;
            }

            World.RegisterSystem(new Draw2DSystem());
        }
        
        private void CreateTilemap(TileInstance[] tiles, string spritePath, float tileSize, float depth)
        {
            foreach (var tile in tiles)
            {
                var tileEntity = new Entity();

                bool flipX = tile.F is 1 or 3  ? true : false;
                bool flipY = tile.F is 2 or 3 ? true : false;
                
                var spriteComponent = new SpriteComponent("Resources/" + spritePath, tileSize, tileSize, flipX, flipY, depth);
                spriteComponent.TileX = tile.Src[0] / tileSize;
                spriteComponent.TileY = tile.Src[1] / tileSize;

                tileEntity.AddComponent(spriteComponent);
                tileEntity.AddComponent(new Position2DComponent((int)tile.Px[0], (int)tile.Px[1]));

                World.AddEntity(tileEntity);
            }
        }
    }
}