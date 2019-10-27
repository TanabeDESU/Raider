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
using OssanAndUmbrella;

namespace GameEngine
{
    public class Map
    {

        public int mapWidth, mapHeight;
        public int glidSize;
        public GameObject[,] mapData;
        public Dictionary<string, GameObject> mapChips;

        public struct Int2
        {
            public int x, y;
            public Int2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

        }

        //汚いアルヨ
        public Map(string fileName, int glidSize)
        {
            var data = CSVReader.ReadFile(fileName, out mapWidth, out mapHeight);
            this.glidSize = glidSize;
#if DEBUG
            Console.WriteLine(mapWidth + ", " + mapHeight);
#endif 
            mapData = new GameObject[mapWidth, mapHeight];
            for (int i = 0; i < mapHeight; i++)
            {
                for (int k = 0; k < mapWidth; k++)
                {
                    if (data[k, i] == "0")
                    {
                        mapData[k, i] = GameManager.instance.nowScene.gameObjectManager.Instantiate(new MapChip_Air(new Transform(new Vector2(k * glidSize, i * glidSize))));
                        Console.WriteLine("ブロック見つけたでぇ～");
                    }
                    else if (data[k, i] == "1")
                    {
                        mapData[k, i] = GameManager.instance.nowScene.gameObjectManager.Instantiate(new MapChip_Block(new Transform(new Vector2(k * glidSize, i * glidSize))));
                        Console.WriteLine("これは・・・空気！！！！");
                    }
                    else
                    {
                        Console.WriteLine("なんやねんこれ→" + data[k, i]);
                    }
                }
            }
        }

        public Int2 GetMapPositon(Vector2 positon)
        {
            int x = (int)((positon.X + 32) / glidSize);
            int y = (int)((positon.Y + 32) / glidSize);
            return new Int2(x, y);
        }

        public GameObject GetMapChip(Int2 mapPosition)
        {
            if (mapPosition.x > mapWidth || mapPosition.x < 0 || mapPosition.y > mapHeight || mapPosition.y < 0) return null;
            return mapData[mapPosition.x, mapPosition.y];
        }

        public GameObject GetMapChip(int x, int y)
        {
            if (x >= mapWidth || x < 0 || y >= mapHeight || y < 0) return null;
            return mapData[x, y];
        }

        public RectangleCollider MapCollision(Rigidbody rigidbody, bool horizontal)
        {
            //グローバル座標をマップ座標に変換
            Int2 position;
            Vector2 tempPosition;
            tempPosition.X = rigidbody.rectangleCollider.square.transform.position.X;
            tempPosition.Y = rigidbody.rectangleCollider.square.transform.position.Y;
            position = GetMapPositon(tempPosition);

            //右側にある上中下3枚のマップチップと衝突判定
            for (int i = -3; i < 4; i++)
            {
                Console.WriteLine("試したるで");

                for (int k = -3; k < 4; k++)
                {
                    GameObject mapChip = GetMapChip(position.x + k, position.y + i);
                    if (mapChip == null)
                    {
                        Console.WriteLine("見つからへん→" + (position.x + k) + ", " + (position.y + i));
                        continue;
                    }
                    RectangleCollider mapCollider = (RectangleCollider)mapChip.GetComponent("RectangleCollider");

                    if (mapCollider.square.Intersects(rigidbody.rectangleCollider.square) && mapCollider != null)
                    {
                        if (horizontal) {
                            rigidbody.HorizontalHitBack(mapCollider);
                        }
                        else
                        {
                            rigidbody.VerticalHitBack(mapCollider);
                        }
                    }
                }
            }
            return null;
        }
    }
}
