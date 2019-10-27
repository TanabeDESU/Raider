using Microsoft.Xna.Framework;
#if LevelDesign
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.UI
{
    class UIBase : GameObject
    {
        Vector2 isolatePos;
        EditModeUI uI;
        public UIBase(EditModeUI editModeUI,Transform transform) : base("uiBase", transform)
        {
            uI = editModeUI;
            isolatePos = transform.position;
            AddComponent(new SpriteRenderer("uiColor",transform, 5));
            StartObject();
        }
        public override GameObject Clone()
        {
            throw new NotImplementedException();
        }

        public override void Hit(RectangleCollider rectangleCollider)
        {
        }

        public override void Start()
        {
        }

        public override void Update()
        {
            transform.position = isolatePos+uI.GetCamera().transform.position;
            gameCompoents["SpriteRenderer"].Update();
        }
    }
}
#endif