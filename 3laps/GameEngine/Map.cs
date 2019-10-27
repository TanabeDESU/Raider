using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _3laps;

namespace GameEngine
{
    public class Map//マップ
    {

        public int mapWidth, mapHeight;//幅、高さ
        public int glidSize;//一マスの大きさ
        public GameObject[,] mapData;//Mapデータ
        public Dictionary<string, GameObject> mapChips;//MapChipを登録する

        public struct Int2
        {
            public int x, y;
            public Int2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

        }

        
        public Map(string fileName, int glidSize)
        {
            var reader = new CSVReader();
            reader.Read(fileName);
            var data = reader.GetIntMatrix();
            mapWidth = data.GetLength(1);
            mapHeight = data.GetLength(0);
            this.glidSize = glidSize;
#if DEBUG
            Console.WriteLine(mapWidth + ", " + mapHeight);
#endif 
            mapData = new GameObject[mapHeight,mapWidth];
            MapGenarate(data);
        }
        public void MapGenarate(int[,] data)
        {
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

        public GameObject GetMapChip(int x, int y)
        {
            if (x >= mapWidth || x < 0 || y >= mapHeight || y < 0) return null;
            return mapData[y, x];
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
            for (int i = -1; i < 2; i++)
            {


                for (int k = -1; k < 2; k++)
                {
                    GameObject mapChip = GetMapChip(position.x + k, position.y + i);
                    if (mapChip == null)
                    {

                        continue;
                    }
                    RectangleCollider mapCollider = (RectangleCollider)mapChip.GetComponent("RectangleCollider");
                    if (mapCollider == null) continue;
                    
                    if (mapCollider.square.Intersects(rigidbody.rectangleCollider.square))
                    {
                        if (horizontal) {
                            rigidbody.HorizontalHitBack(mapCollider);
                        }
                        else
                        {
                            rigidbody.VerticalHitBack(mapCollider);
                        }
                        result = true;
                    }
                    rigidbody.rectangleCollider.gameObject.Hit(mapCollider);
                    mapCollider.gameObject.Hit(rigidbody.rectangleCollider);
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
            for (int i = -1; i < 2; i++)
            {
                for (int k = -1; k < 2; k++)
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
