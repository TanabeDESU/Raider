using Microsoft.Xna.Framework;
using System;
#if LevelDesign
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.UI
{
    class PlayButton : GameObject,IButton
    {
        EditModeUI uI;
        Rectangle rect;
        Vector2 isolatePos;
        public PlayButton(EditModeUI editModeUI):base("playButton", new Transform(new Vector2(ScreenInformations.ScreenWidth/2, ScreenInformations.ButtonSpace+ScreenInformations.ButtonHeight/2)))
        {
            uI = editModeUI;
            AddComponent( new ShaderEffectRenderer("playButton",transform, 5,"None"));
            isolatePos = transform.position;
            StartObject();
            rect = new Rectangle(ScreenInformations.ScreenWidth / 2-ScreenInformations.ButtonWidth/2, ScreenInformations.ButtonSpace, 
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
            uI.PlayOn();
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            
            transform.position = isolatePos+uI.GetCamera().transform.position;
            gameCompoents["ShaderRenderer"].Update();
        }
    }
}
#endif