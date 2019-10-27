using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace GameEngine
{
    class AudioPlayer : GameComponent
    {
        SoundEffectInstance soundEffectInstance;
        int timer = 0, maxTime;
        string assetName;
        public bool playOnAwake, isLoop, isPlay;
        private bool isPause = false;
        public AudioPlayer(string assetName, bool playOnAwake, bool isLoop, float timeSeconds = 0)
        {
            if (!Audio.instance.sounds.ContainsKey(assetName))
            {
                Audio.instance.LoadContent(assetName);
            }
            this.assetName = assetName;
            this.playOnAwake = playOnAwake;
            this.isLoop = isLoop;
            maxTime = (int)timeSeconds * 60;
            soundEffectInstance = Audio.instance.sounds[assetName].CreateInstance();
        }
        public override void Start()
        {
            if (playOnAwake)
            {
                soundEffectInstance.Play();
                isPlay = true;
            }
        }

        public void Play()
        {
            if (isPause)
            {
                soundEffectInstance.Resume();
                isPause = false;
            }
            else
            {
                soundEffectInstance.Play();
            }
            isPlay = true;
        }

        public void Replay()
        {
            soundEffectInstance.Stop();
            soundEffectInstance.Play();
            isPause = false;
            isPlay = true;
        }

        public void Pause()
        {
            soundEffectInstance.Pause();
            isPause = true;
            isPlay = false;
        }

        public void Stop()
        {
            soundEffectInstance.Stop();
            timer = 0;
            isPlay = false;
        }

        public override string ToString()
        {
            return "AudioPlayer";
        }

        public override void Update()
        {
            if (!isPlay) return;
            if (timer > maxTime)
            {
                if (isLoop)
                {
                    soundEffectInstance.Play();
                    timer = 0;
                }
            }
            else
            {
                timer++;
            }

        }


    }
}
