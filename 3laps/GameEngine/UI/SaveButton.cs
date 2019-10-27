#if LevelDesign
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.UI
{
    class SaveButton : GameObject,IButton
    {
        EditModeUI uI;
        Rectangle rect;
        Vector2 isolatePos;
        public SaveButton(EditModeUI editModeUI):base("saveButton", new Transform(new Vector2(ScreenInformations.ButtonSpace+ScreenInformations.ButtonWidth/2, ScreenInformations.ButtonSpace+ScreenInformations.ButtonHeight/2)))
        {
            uI = editModeUI;
            AddComponent( new SpriteRenderer("saveButton", transform, 5));
            isolatePos = transform.position;
            StartObject();
        rect = new Rectangle(ScreenInformations.ButtonSpace, ScreenInformations.ButtonSpace,
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
            uI.Export();
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
#endif