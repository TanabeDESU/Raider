#if LevelDesign
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.UI
{
    class MapChipButton : GameObject, IButton
    {
        EditModeUI uI;
        int chipNum;
        Rectangle rect;
        Vector2 isolatePos;
        public MapChipButton(Vector2 pos,string name,int num,EditModeUI editModeUI) : base("mapChipButton_"+name,new Transform(pos))
        {
            chipNum = num; uI = editModeUI;
            AddComponent(new SpriteRenderer(name, transform, 5));
            StartObject();
            isolatePos = pos;
            rect = new Rectangle((int)transform.position.X- ScreenInformations.ButtonWidth / 2,(int)transform.position.Y-ScreenInformations.ButtonHeight/2,
                ScreenInformations.ButtonWidth, ScreenInformations.ButtonHeight);
        }
        public override GameObject Clone()
        {
            throw new NotImplementedException();
        }

        public void Draw()
        {
        }

        public Rectangle GetRect()
        {
            return rect;
        }

        public override void Hit(RectangleCollider rectangleCollider)
        {
            throw new NotImplementedException();
        }

        public void Off()
        {
        }

        public void On()
        {
        }

        public void OnClick()
        {
            uI.SetNum(chipNum);
        }

        public override void Start()
        {
        }

        public override void Update()
        {
            transform.position = isolatePos + uI.GetCamera().transform.position;
            gameCompoents["SpriteRenderer"].Update();
        }
    }
}
# endif