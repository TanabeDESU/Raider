using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace GameEngine
{
    public class Audio//音声版のRenderer 音声の読み込みに使う
    {
        public static Audio instance;
        private ContentManager contentManager;
        public Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();

        public Audio(ContentManager content)
        {
            contentManager = content;
            instance = this;
        }

        public void LoadContent(string assetName)
        {
            if (sounds.ContainsKey(assetName)) return;
            sounds.Add(assetName, contentManager.Load<SoundEffect>(assetName));
        }
        
    }
}
