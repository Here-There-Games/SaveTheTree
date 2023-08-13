using UnityEngine;
using UnityEngine.UI;

namespace Common
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
                musicImage.sprite = mute;
            }
            else{
                musicInGame.Play();
                musicImage.sprite = unmute;
            }
        }
    }
}