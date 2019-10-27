using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    class StageIcon : GameObject
    {
        int stageNum, stageSum;

        public StageIcon(int stageNum, int stageSum) : base("StageIcon" + stageNum, new Transform(new Vector2(1920 / (stageSum + 1) * stageNum + 1), 1080 + 540))
        {
            this.stageNum = stageNum;
            this.stageSum = stageSum;
        }

        public override GameObject Clone()
        {
            return new StageIcon(stageNum, stageSum);
        }

        public override void Hit(RectangleCollider rectangleCollider)
        {
        }

        public override void Start()
        {
        }

        public override void Update()
        {
        }
    }
}
