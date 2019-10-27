using Microsoft.Xna.Framework;
using System;

namespace GameEngine
{
    public class Camera : GameObject//カメラ　描画範囲の変更
    {
        Player player;
        bool chasePlayer;
        Vector2 velocity;
        const float MAX_LINE = 480, START_LINE = 240, CENTER_X = 960, CENTER_Y = 540, MAX_DISTANCE = MAX_LINE - START_LINE;
        float distance;
        private float zoomSize;
        private Vector2 pos, topPos, downPos;
        public float ZoomSize { get => zoomSize; set => zoomSize = value; }

        public Camera(Transform transform, float zoomSize, bool chasePlayer):base("Camera", transform)
        {
            this.ZoomSize = zoomSize;
            Renderer.instance.camera = this;
            pos = transform.position;
            topPos = pos;
            topPos.Y -= 10;
            downPos = pos;
            downPos.Y += 10;
            this.chasePlayer = chasePlayer;
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            Quake();
            if(Player.instance != null && chasePlayer)
            {
                ChasePlayer();
            }
        }

        public override void Hit(RectangleCollider rectangleCollider)
        {
            
        }
        int t;
        public bool setQuake;
        public void Quake()
        {
            if (setQuake)
            {
                t++;
                if (t < 3)
                {
                    transform.position.Y = Easing.QuadInOut(t, pos.Y, topPos.Y, 2);
                }
                else if(t < 5)
                {
                    transform.position.Y = Easing.QuadInOut(t - 2, topPos.Y, downPos.Y, 2);
                }
                else if(t < 7)
                {
                    transform.position.Y = Easing.QuadInOut(t - 6, downPos.Y, pos.Y, 2);
                }
                else if(t < 9)
                {
                    setQuake = false;
                    transform.position.Y = pos.Y;
                    t = 0;
                }
            }
        }
        float targetPos;
        void ChasePlayer()
        {
            float cameraPos = Player.instance.transform.position.X - 960 + MAX_LINE;
            float distance = targetPos - cameraPos;
            if(Player.instance.playerState != Player.PlayerState.Drop && Player.instance.playerState == Player.PlayerState.Drop)
            {
                if(distance > 0)
                {
                    if(distance > 100)
                    {
                        targetPos -= 40f;
                    }
                    else
                    {
                        targetPos -= Math.Abs(Player.instance.rb.velocity.X) + 2;
                    }
                    targetPos -= Math.Abs(Player.instance.rb.velocity.X) + 2;
                    if (targetPos < cameraPos)
                    {

                        targetPos = cameraPos;
                    }
                }
                else if(distance < 0)
                {
                    if (distance < -100)
                    {
                        targetPos += 40f;
                    }
                    else
                    {
                        targetPos += Math.Abs(Player.instance.rb.velocity.X) + 2;
                    }
                    if (targetPos > cameraPos)
                    {
                        targetPos = cameraPos;
                    }
                }
            }
            else
            {
                targetPos = Player.instance.transform.position.X - 960;
            }
            transform.position.X = MathHelper.Lerp(transform.position.X, targetPos, 0.2f);
            /*distance = transform.position.X + MAX_LINE - Player.instance.transform.position.X;
            if(distance > MAX_DISTANCE)
            {
                distance = MAX_DISTANCE;
                transform.position.X = Player.instance.transform.position.X - 960 + START_LINE;
            }
            velocity.X = (1 - (distance / MAX_DISTANCE)) * Player.instance.rb.velocity.X;
            transform.position.X += velocity.X;*/
        }

        public override GameObject Clone()
        {
            GameObject clone = (GameObject)MemberwiseClone();
            clone.transform = transform.Clone();
            clone.gameCompoents = CloneComponets(clone);
            return clone;
        }
    }
}
