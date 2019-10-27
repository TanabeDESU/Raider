using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using GameEngine.MapChip;
using Microsoft.Xna.Framework;


namespace _3laps
{
    public class MapChip_Air : GameObject,IMapChip//当たり判定がないMapChip
    {
        public MapChip_Air(Transform transform) : base("MapChip_Air", transform)
        {
            tag = "Map_Air";
            //AddComponent(new SpriteRenderer("Air", transform, 0, Color.White, 1, 64));
            AddComponent(new RectangleCollider(this, new Square(transform, 64)));
            RectangleCollider rectangleCollider =  (RectangleCollider)GetComponent("RectangleCollider");
            rectangleCollider.isTrigger = true;
        }

        public override void Start()
        {
        }

        public override void Update()
        {

        }

        public override void Hit(RectangleCollider rectangleCollider)
        {
        }

        public override GameObject Clone()
        {
            GameObject clone = (GameObject)MemberwiseClone();
            clone.transform = transform.Clone();
            clone.gameCompoents = CloneComponets(clone);
            return clone;
        }

        public void Draw()
        {
        }
    }
}
