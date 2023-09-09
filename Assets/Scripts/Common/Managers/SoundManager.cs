using UnityEngine;
using UnityEngine.UI;

namespace Common.Managers
{
    public class SoundManager : BaseSingleton<SoundManager>
    {
        [SerializeField] private AudioSource musicInGame;
        [SerializeField] private Image musicImage;
        [SerializeField] private Sprite mute;
        [SerializeField] private Sprite unmute;

        private bool playing => musicInGame.isPlaying;

        public void ChangeMusicMute()
        {
            
            if(playing){
                musicInGame.Stop();
                if(musicImage != null)
                    musicImage.sprite = mute;
            }
            else{
                musicInGame.Play();
                if(musicImage != null)
                    musicImage.sprite = unmute;
            }
        }
    }
}