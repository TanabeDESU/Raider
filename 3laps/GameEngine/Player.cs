using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    public class Player : GameObject
    {
        public Rigidbody rb;
        const float GROUND_SPEED = 5, AIR_SPEED = 15, JUMP_FORCE = 13, PLAYER_HEIGHT = 64, DROP_FORCE = 20;
        const float GROUND_POSITION = ((64 * 18) / 2 )- 32;
        float gravityDirection = 1;
        public bool overGround = true;
        public bool dropFlg;
        public static Player instance;

        public enum PlayerState
        {
            Ground,
            Air,
            Drop
        }

        public PlayerState playerState = PlayerState.Ground;

        public Player(Transform transform) : base("Player", transform)
        {
            instance = this;
            AddComponent(new SpriteRenderer("TempPlayer", transform, 1));
            AddComponent(new RectangleCollider(this, new Square(transform, 64)));
            AddComponent(new Rigidbody(transform, (RectangleCollider)GetComponent("RectangleCollider")));
            AddComponent(new CircleWarpShaderEffectGenerater(4));
            AddComponent(new UnderWorldShaderController(0.06f));
            //AddComponent(new ConcentratedLineShaderController(4));
            //AddComponent(new ConcentratedLineShaderController(3));
            //AddComponent(new ConcentratedLineShaderController(2));
            //AddComponent(new ConcentratedLineShaderController(1));
            //AddComponent(new ConcentratedLineShaderController(0));
            rb = (Rigidbody)GetComponent("Rigidbody");
        }

        public override GameObject Clone()
        {
            return null;
        }

        public override void Hit(RectangleCollider rectangleCollider)
        {
            if(rectangleCollider.gameObject.tag == "Map_UpStream")
            {
                if (!overGround)
                {
                    Drop();
                }
                else
                {
                    playerState = PlayerState.Air;
                    rb.velocity.Y = -JUMP_FORCE;
                }
            }
            else if (rectangleCollider.gameObject.tag == "Map_DownStream")
            {
                if (overGround)
                {
                    Drop();
                }
                else
                {
                    playerState = PlayerState.Air;
                    rb.velocity.Y = JUMP_FORCE;
                }
            }
            else if(rectangleCollider.gameObject.tag == "Map_TopNeedle")
            {
                if (overGround && rb.velocity.Y >= 0)
                {
                    isDead = true;
                }
            }
            else if(rectangleCollider.gameObject.tag == "Map_BottomNeedle")
            {
                if(!overGround && rb.velocity.Y <= 0)
                {
                    isDead = true;
                }
            }
            else if(rectangleCollider.gameObject.tag == "Map_Ground")
            {
                if (playerState == PlayerState.Drop && rb.velocity.Y == 0)
                {
                    playerState = PlayerState.Ground;
                }
            }
        }

        public void DropBlock(RectangleCollider rectangleCollider)
        {
            float moveDistance; 
            if (overGround)
            {
                if(rectangleCollider.gameObject.transform.position.Y < GROUND_POSITION)
                {
                    moveDistance = -64 - (PLAYER_HEIGHT / 2) - 1;
                }
                else
                {
                    moveDistance = -(PLAYER_HEIGHT / 2) - 1;
                }
            }
            else
            {
                if (rectangleCollider.gameObject.transform.position.Y > GROUND_POSITION)
                {
                    moveDistance = 64 + (PLAYER_HEIGHT / 2) + 1;
                }
                else
                {
                    moveDistance = (PLAYER_HEIGHT / 2) + 1;
                }
            }
            
            transform.position.Y = GROUND_POSITION + moveDistance;
        }
        public void DropTopNeedle(RectangleCollider rectangleCollider)
        {
            if (overGround && !dropFlg)
            {
                isDead = true;
            }
            else
            {
                SuccessDrop();
            }
        }
        public void DropBottomNeedle(RectangleCollider rectangleCollider)
        {
            if(!overGround && !dropFlg)
            {
                isDead = true;
            }
            else
            {
                SuccessDrop();
            }
        }
        public void DropUpStream(RectangleCollider rectangleCollider)
        {
            if (overGround)
            {
                transform.position.Y = GROUND_POSITION - (PLAYER_HEIGHT / 2);
                rb.velocity.Y = -JUMP_FORCE;
            }
            else
            {
                SuccessDrop();
            }
        }
        public void DropDownStream(RectangleCollider rectangleCollider)
        {
            if (!overGround)
            {
                transform.position.Y = GROUND_POSITION + (PLAYER_HEIGHT / 2);
                rb.velocity.Y = JUMP_FORCE;
            }
            else
            {
                SuccessDrop();
            }
        }

        public override void Start()
        {
        }
        public override void Update()
        {
            dropFlg = false;
            switch (playerState)
            {
                case PlayerState.Ground:
                    rb.velocity.X = GROUND_SPEED;
                    GroundCollision();
                    break;
                case PlayerState.Air:
                    rb.velocity.X = AIR_SPEED;
                    GroundCollision();
                    break;
                case PlayerState.Drop:
                    rb.velocity.X = 0;
                    rb.velocity.Y = DROP_FORCE * gravityDirection;
                    if(overGround)
                    {
                        if(transform.position.Y > GROUND_POSITION)
                        {
                            playerState = PlayerState.Air;
                            transform.position.Y = GROUND_POSITION + (PLAYER_HEIGHT / 2);
                            overGround = !overGround;
                            rb.velocity.Y = JUMP_FORCE * gravityDirection;
                            ChangeGravity();
                        }
                        
                    }
                    else
                    {
                        if (transform.position.Y < GROUND_POSITION)
                        {
                            playerState = PlayerState.Air;
                            transform.position.Y = GROUND_POSITION - (PLAYER_HEIGHT / 2);
                            overGround = !overGround;
                            rb.velocity.Y = JUMP_FORCE * gravityDirection;
                            ChangeGravity();
                        }
                    }
                    break;
            }
            if(Input.GetKeyDown(Keys.Space))
            {
                ((CircleWarpShaderEffectGenerater)gameCompoents["CircleWarpGenerater"]).AddWarp(transform.position, 200, 30, 6);
                ((UnderWorldShaderController)gameCompoents["UnderWorldShaderController"]).Switch();
                //((ConcentratedLineShaderController)gameCompoents["ConcentratedLineShaderController"]).On(0.5f, 0.5f, 0.06f);
                //((ConcentratedLineShaderController)gameCompoents["ConcentratedLineShaderController2"]).On(0.5f, 0.5f, 0.06f);
                //((ConcentratedLineShaderController)gameCompoents["ConcentratedLineShaderController3"]).On(0.5f, 0.5f, 0.06f);
                //((ConcentratedLineShaderController)gameCompoents["ConcentratedLineShaderController4"]).On(0.5f, 0.5f, 0.06f);
                Drop();
            }
            

        }

        public void ChangeGravity()
        {
            gravityDirection *= -1;
            Renderer.instance.camera.setQuake = true;
            rb.gravityScale = gravityDirection;
        }

        public void Drop()
        {
            playerState = PlayerState.Drop;
            /*float distance = transform.position.Y - GROUND_POSITION;
            if (overGround)
            {
                distance += 64;
            }
            else
            {
                distance -= 64;
            }
            Transform colliderTransform = new Transform(new Vector2(transform.position.X, GROUND_POSITION + (distance / 2)));
            Console.WriteLine(distance);
            DropCollider dropCollider = new DropCollider(colliderTransform, this, distance);
            bool result = GameManager.instance.nowScene.nowMap.MapCollisionToPixel(dropCollider.rb, true);
            if(result == false)
            {
                SuccessDrop();
            }*/
        }

        public void SuccessDrop()
        {
            if (dropFlg) return;
            dropFlg = true;
            if (overGround)
            {
                transform.position.Y = GROUND_POSITION + (PLAYER_HEIGHT / 2);
            }
            else
            {
                transform.position.Y = GROUND_POSITION - (PLAYER_HEIGHT / 2);
            }
            rb.velocity.Y = JUMP_FORCE * gravityDirection;
            overGround = !overGround;
            playerState = PlayerState.Air;
            ChangeGravity();
        }

        public void GroundCollision()
        {
            if(overGround)
            {
                if(transform.position.Y > GROUND_POSITION - (PLAYER_HEIGHT / 2))
                {
                    transform.position.Y = GROUND_POSITION - (PLAYER_HEIGHT / 2);
                    rb.velocity.Y = 0;
                    if(playerState == PlayerState.Air)
                    {
                        playerState = PlayerState.Ground;
                    }
                }
            }
            else
            {
                if (transform.position.Y < GROUND_POSITION + (PLAYER_HEIGHT / 2))
                {
                    transform.position.Y = GROUND_POSITION + (PLAYER_HEIGHT / 2);
                    rb.velocity.Y = 0;
                    if (playerState == PlayerState.Air)
                    {
                        playerState = PlayerState.Ground;
                    }
                }
            }
        }
    }
}
