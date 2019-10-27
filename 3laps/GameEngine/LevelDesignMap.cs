using _3laps;
using GameEngine;
using GameEngine.MapChip;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class LevelDesignMap
    {
        public int mapWidth, mapHeight;//幅、高さ
        public int glidSize;//一マスの大きさ
        public GameObject[,] mapData;//Mapデータ
        public Dictionary<string, GameObject> mapChips;//MapChipを登録する
        public int [,]mapChipData;
        GameObjectManager objectManager;
        string mapName;
        public struct Int2
        {
            public int x, y;
            public Int2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        public LevelDesignMap(string fileName, int glidSize,GameObjectManager gameObjectManager)
        {
            mapName = fileName;
            var reader = new CSVReader();
            reader.Read(fileName);
            var data = reader.GetIntMatrix();
            mapChipData = data;
            this.glidSize = glidSize;
            mapWidth = data.GetLength(1);
            mapHeight = data.GetLength(0);
#if DEBUG
#endif 
            mapData = new GameObject[mapHeight,mapWidth];
            MapGenarate(data);
            objectManager = gameObjectManager;
        }
        private void MapObjectsReset()
        {
            for (int i = 0; i < mapData.GetLength(0); i++)
            {
                for (int k = 0; k < mapData.GetLength(1); k++)
                {
                    if(mapData[i,k]!=null)
                    mapData[i, k].isDead = true;
                    GameManager.instance.nowScene.gameObjectManager.Destroy(mapData[i, k]);
                    mapData[i, k] = null;
                }
            }
            GameManager.instance.nowScene.gameObjectManager.RemoveObj();
            mapData = new GameObject[mapHeight, mapWidth];
        }
        public void MapGenarate(int[,] data)
        {
            MapObjectsReset();
            for (int i = 0; i < mapHeight; i++)
            {
                for (int k = 0; k < mapWidth; k++)
                {
                    if (data[i, k] == 0)
                    {
                        mapData[i, k] = GameManager.instance.nowScene.gameObjectManager.Instantiate(new MapChip_Air(new Transform(new Vector2(k * glidSize, i * glidSize))));//ここに数値入れて補正
                    }
                    else if (data[i, k] == 1)
                    {
                        mapData[i, k] = GameManager.instance.nowScene.gameObjectManager.Instantiate(new MapChip_Block(new Transform(new Vector2(k * glidSize, i * glidSize))));//ここに数値入れて補正

                    }
                    else if (data[i, k] == 2)
                    {
                        mapData[i, k] = GameManager.instance.nowScene.gameObjectManager.Instantiate(new MapChip_TopNeedle(new Transform(new Vector2(k * glidSize, i * glidSize))));
                    }
                    else if (data[i, k] == 3)
                    {
                        mapData[i, k] = GameManager.instance.nowScene.gameObjectManager.Instantiate(new MapChip_BottomNeedle(new Transform(new Vector2(k * glidSize, i * glidSize))));
                    }
                    else if (data[i, k] == 4)
                    {
                        mapData[i, k] = GameManager.instance.nowScene.gameObjectManager.Instantiate(new MapChip_UpStream(new Transform(new Vector2(k * glidSize, i * glidSize))));
                    }
                    else if (data[i, k] == 5)
                    {
                        mapData[i, k] = GameManager.instance.nowScene.gameObjectManager.Instantiate(new MapChip_DownStream(new Transform(new Vector2(k * glidSize, i * glidSize))));
                    }
                    else
                    {
                        Console.WriteLine(mapData[i, k]);
                    }
                }
            }
        }
        public Int2 GetMapPositon(Vector2 positon)
        {
            int x = (int)((positon.X + (glidSize / 2)) / glidSize);
            int y = (int)((positon.Y + (glidSize / 2)) / glidSize);
            return new Int2(x, y);
        }
        
        public GameObject GetMapChip(Int2 mapPosition)
        {
            if (mapPosition.x > mapWidth || mapPosition.x < 0 || mapPosition.y > mapHeight || mapPosition.y < 0) return null;
            return mapData[mapPosition.y, mapPosition.x];
        }
        public void ChangeGrid(Point point,int chipNum)
        {
            int x = (point.X)/glidSize;
            int y = (point.Y ) / glidSize;
            if (x >= mapWidth || x < 0 || y >= mapHeight || y < 0)
                return;
            if (mapChipData[y, x] == chipNum) return;
            mapChipData[y, x] = chipNum;
            MapGenarate(mapChipData);
            foreach(var m in mapData)
            {
                m.StartObject();
            }
        }
        public void DrawMap()
        {
            for(int i = 0; i < mapData.GetLength(0); i++)
            {
                for(int j = 0; j < mapData.GetLength(1); j++)
                {
                    ((IMapChip)mapData[i, j]).Draw();
                }
            }
        }
        public void Export(string exportFileName="test.txt")
        {
            var csvWriter = new CSVWriter();
            csvWriter.Write(exportFileName, mapChipData);
        }
        public GameObject GetMapChip(int x, int y)
        {
            if (x >= mapWidth || x < 0 || y >= mapHeight || y < 0) return null;
            return mapData[x, y];
        }

        public bool MapCollision(Rigidbody rigidbody, bool horizontal)
        {
            //グローバル座標をマップ座標に変換
            Int2 position;
            Vector2 tempPosition;
            tempPosition.X = rigidbody.rectangleCollider.square.transform.position.X;
            tempPosition.Y = rigidbody.rectangleCollider.square.transform.position.Y;
            position = GetMapPositon(tempPosition);

            bool result = false;
            //右側にある上中下3枚のマップチップと衝突判定
            for (int i = -3; i < 4; i++)
            {


                for (int k = -3; k < 4; k++)
                {
                    GameObject mapChip = GetMapChip(position.x + k, position.y + i);
                    if (mapChip == null)
                    {

                        continue;
                    }
                    RectangleCollider mapCollider = (RectangleCollider)mapChip.GetComponent("RectangleCollider");

                    if (mapCollider.square.Intersects(rigidbody.rectangleCollider.square) && mapCollider != null)
                    {
                        if (horizontal)
                        {
                            rigidbody.HorizontalHitBack(mapCollider);
                        }
                        else
                        {
                            rigidbody.VerticalHitBack(mapCollider);
                        }
                        result = true;
                    }
                }
            }
            return result; ;
        }

        public bool MapCollision(Rigidbody rigidbody, bool horizontal, List<string> filterTags)
        {
            //グローバル座標をマップ座標に変換
            Int2 position;
            Vector2 tempPosition;
            tempPosition.X = rigidbody.rectangleCollider.square.transform.position.X;
            tempPosition.Y = rigidbody.rectangleCollider.square.transform.position.Y;
            position = GetMapPositon(tempPosition);

            bool result = false;
            for (int i = -3; i < 4; i++)
            {
                for (int k = -3; k < 4; k++)
                {
                    GameObject mapChip = GetMapChip(position.x + k, position.y + i);
                    if (mapChip == null)
                    {

                        continue;
                    }
                    RectangleCollider mapCollider = (RectangleCollider)mapChip.GetComponent("RectangleCollider");

                    if (mapCollider.square.Intersects(rigidbody.rectangleCollider.square) && mapCollider != null)
                    {
                        mapCollider.gameObject.Hit(rigidbody.rectangleCollider);
                        rigidbody.rectangleCollider.gameObject.Hit(mapCollider);
                        if (filterTags.Contains(mapCollider.gameObject.tag))
                        {
                            if (horizontal)
                            {
                                rigidbody.HorizontalHitBack(mapCollider);
                            }
                            else
                            {
                                rigidbody.VerticalHitBack(mapCollider);
                            }
                            result = true;
                        }
                    }
                }
            }
            return result; ;
        }
        public void ChangeYSize(int y)
        {
            mapHeight += y;
            var newMapData = new int[mapHeight, mapWidth];
            if (y < 0)
                newMapData = MapShrink();
            else
                newMapData = MapExpansion();
            mapChipData = newMapData;
            MapGenarate(mapChipData);
            foreach (var m in mapData)
            {
                m.StartObject();
            }
        }
        public void ChangeXSize(int x)
        {
            mapWidth += x;
            var newMapData = new int[mapHeight, mapWidth];
            if (x < 0)
                newMapData = MapShrink();
            else
                newMapData = MapExpansion();
            Array.Clear(mapData, 0, 0);
            mapChipData = newMapData;
            MapGenarate(mapChipData);
            foreach (var m in mapData)
            {
                m.StartObject();
            }
        }
        private int[,] MapShrink()
        {
            var newMapData = new int[mapHeight, mapWidth];
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    newMapData[i, j] = mapChipData[i, j];
                }

            }
            return newMapData;
        }
        private int[,] MapExpansion()
        {
            var newMapData = new int[mapHeight, mapWidth];
            for (int i = 0; i <mapChipData.GetLength(0); i++)
            {
                for (int j = 0; j < mapChipData.GetLength(1); j++)
                {
                    newMapData[i, j] = mapChipData[i, j];
                }
            }
            return newMapData;
        }
        public string GetNowMap()
        {
            return mapName;
        }
        public Map Clone(GameObjectManager gameObjectManager)
        {
            Map clone = (Map)MemberwiseClone();
            GameObject[,] cloneMapData = new GameObject[mapWidth, mapHeight];
            for (int i = 0; i < mapHeight; i++)
            {
                for (int k = 0; k < mapWidth; k++)
                {
                    GameObject cloneMapChip = mapData[k, i].Clone();
                    cloneMapData[k, i] = cloneMapChip;
                    gameObjectManager.objectList.Add(cloneMapChip);
                }
            }
            clone.mapData = cloneMapData;
            return clone;
        }
    }

}

