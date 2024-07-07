using Common.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common
{
    [System.Serializable]
    public class SpawnerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text waveReload;
        [SerializeField] private TMP_Text waveCount;
        [SerializeField] private string title = "Wave";
        [SerializeField] private string separator = " ";
        [SerializeField] private AnimationCurve animationCurve;

        private SpawnerManager spawnerManager;
        private int currentWave => spawnerManager.WaveCount;
        private Animation changeAnimation;

        public void Start()
        {
            changeAnimation = GetComponent<Animation>();
            
            spawnerManager = SpawnerManager.Instance;

            spawnerManager.OnUpdateWaveEvent += StartWave;

            Color waveColor = waveReload.color;
            waveReload.color = new Color(waveColor.r, waveColor.g, waveColor.b, 0);
        }

        private void StartWave(int wave)
        {
            waveCount.text = $"{title}{separator}{wave}";
            waveReload.text = $"Wave: {wave}";
            changeAnimation.Play();
        }
    }
}