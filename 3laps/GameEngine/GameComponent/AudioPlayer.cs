using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace GameEngine
{
    class AudioPlayer : GameComponent//音声を再生するクラス
    {
        public SoundEffectInstance soundEffectInstance;//音声ファイル
        int timer = 0, maxTime;//再生時間
        string assetName;//名前
        public bool playOnAwake, isLoop, isPlay;//playOnAwake 生成時に鳴らすか, isLoop 繰り返すか, isPlay鳴らしているか
        private bool isPause = false;//止めるかどうか
        public AudioPlayer(string assetName, bool playOnAwake, bool isLoop, int timeSeconds = 0)
        {
            
            if (!Audio.instance.sounds.ContainsKey(assetName))
            {
                Audio.instance.LoadContent(assetName);
            }
            this.assetName = assetName;
            this.playOnAwake = playOnAwake;
            this.isLoop = isLoop;
            maxTime = (int)(timeSeconds * 60);
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

        public void Play()//再生
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

        public void Replay()//最初から再生
        {
            soundEffectInstance.Stop();
            soundEffectInstance.Play();
            isPause = false;
            isPlay = true;
        }

        public void Pause()//止める
        {
            soundEffectInstance.Pause();
            isPause = true;
            isPlay = false;
        }

        public void Stop()//完全停止
        {
            soundEffectInstance.Stop();
            timer = 0;
            isPlay = false;
        }

        public override string ToString()
        {
            return "AudioPlayer";
        }

        public override void Update()//再生処理
        {
            soundEffectInstance.Play();
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

        public override GameComponent Clone(GameObject obj)
        {
            AudioPlayer clone = (AudioPlayer)MemberwiseClone();
            clone.soundEffectInstance = Audio.instance.sounds[assetName].CreateInstance();
            return clone;
        }

    }
}
