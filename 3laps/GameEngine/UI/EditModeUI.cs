#if LevelDesign
using GameEngine.MapChip;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.UI
{
    class EditModeUI
    {
        Scene scene;
        UIBase uiBase;
        UIBase varticUiBase;
        List<IButton> mapChipButtons;
        List<IButton> buttons;
        PlayButton playButton;
        int mapChipNum;
        public EditModeUI(Scene scene)
        {
            this.scene = scene;
            playButton = new PlayButton(this);
            mapChipButtons = new List<IButton>();
            buttons = new List<IButton>();
            uiBase = new UIBase(this, new Transform(new Vector2(0, 0), 0, new Vector2(ScreenInformations.ScreenWidth, ScreenInformations.ButtonSpace * 2 + ScreenInformations.ButtonHeight)));
            varticUiBase = new UIBase(this, new Transform(new Vector2(ScreenInformations.PlayWindow.Width, 0), 0, new Vector2(ScreenInformations.ButtonSpace * 3 + ScreenInformations.ButtonWidth * 2, ScreenInformations.ScreenHeight)));
            buttons.Add(new SaveButton(this));

            AddMapChipButton(0);
            AddMapChipButton(1);
            AddMapChipButton(2);
            AddMapChipButton(3);
            AddMapChipButton(4);
            AddMapChipButton(5);
            //AddMapChipButton(0);
            //mapChipButtons.Add(new MapChipButton(new Vector2(ScreenInformations.PlayWindow.Width + ScreenInformations.ButtonSpace + ScreenInformations.ButtonWidth / 2,
            //    ScreenInformations.ButtonSpace * 3 + ScreenInformations.ButtonHeight), "Ground", 1, this));
            //mapChipButtons.Add(new MapChipButton(new Vector2(ScreenInformations.PlayWindow.Width + ScreenInformations.ButtonSpace*2+ScreenInformations.ButtonWidth + ScreenInformations.ButtonWidth / 2,
            //     ScreenInformations.ButtonSpace * 3 + ScreenInformations.ButtonHeight), "blockFrame", 0, this));
        }
        public void AddMapChipButton(int mapChipNum)
        {
            int count = mapChipButtons.Count ;
            mapChipButtons.Add(new MapChipButton(new Vector2(ScreenInformations.PlayWindow.Width+ ScreenInformations.ButtonSpace*(count%2+1)+ScreenInformations.ButtonWidth*(count%2)
            +ScreenInformations.ButtonWidth/2,
            ScreenInformations.ButtonSpace*3+ScreenInformations.ButtonHeight*(count/2+1)+ScreenInformations.ButtonSpace*(count/2)
                ),MapChipArray.MapChipButtonName[mapChipNum],mapChipNum,this));
        }
        public void Click(Point point)
        {
            foreach (var b in mapChipButtons)
            {

                if (b.GetRect().Contains(point))
                {
                    mapChipButtons.ForEach(bu => bu.Off());
                    b.OnClick();
                    b.On();
                    return;
                }
            }
            foreach (var b in buttons)
            {

                if (b.GetRect().Contains(point))
                {
                    buttons.ForEach(bu => bu.Off());
                    b.OnClick();
                    b.On();
                    return;
                }
            }
        }
        public void Draw()
        {
            uiBase.Update();
            varticUiBase.Update();
            playButton.Update();
            mapChipButtons.ForEach(b => b.Update());
            buttons.ForEach(b => b.Update());
        }
        public void Export()
        {
            ((StageDesignScene)scene).Export();
        }
        public Camera GetCamera()
        {
            return ((StageDesignScene)scene).GetCamera();
        }
        public int GetMapchipNum()
        {
            return mapChipNum;
        }
        public void MapXChange(int size)
        {
            ((StageDesignScene)scene).ChangeXSize(size);
        }
        public void MapYChange(int size)
        {
            ((StageDesignScene)scene).ChangeYSize(size);
        }
        public void PlayOn()
        {
            ((StageDesignScene)scene).PlaySwitch();
        }
        public void PlayButtonClick(Point point)
        {
            if (playButton.GetRect().Contains(point))
                playButton.OnClick();
        }
        public void SetNum(int num)
        {
            mapChipNum = num;
        }
    }
}
#endif