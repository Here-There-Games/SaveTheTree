using Common.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common
{
    [System.Serializable]
    public class SpawnerView
    {
        [SerializeField] private TMP_Text waveReload;
        [SerializeField] private TMP_Text waveCount;
        [SerializeField] private string title = "Wave";
        [SerializeField] private string separator = " ";
        [FormerlySerializedAs("animationShow"),SerializeField] private AnimationCurve animationCurve;

        private SpawnerManager spawnerManager;
        private int currentWave => spawnerManager.WaveCount;

        public void Start()
        {
            spawnerManager = SpawnerManager.Instance;
            
            spawnerManager.OnUpdateWaveEvent += UpdateWaveCount;
            spawnerManager.WaveTimer.OnStartEvent += StartWave;
            spawnerManager.WaveTimer.OnUpdatedNormalizedEvent += UpdateReloadWave;

            Color waveColor = waveReload.color;
            waveReload.color = new Color(waveColor.r, waveColor.g, waveColor.b, 0);
        }

        private void StartWave()
        {
            if(waveReload != null)
                waveReload.text = $"{title}{separator}{currentWave + 1}";
        }

        private void UpdateReloadWave(float time)
        {
            Color waveColor = waveReload.color;
            waveColor.a = animationCurve.Evaluate(time);
            waveReload.color = waveColor;
        }

        private void UpdateWaveCount(int wave)
        {
            waveCount.text = $"Wave: {currentWave}";
        }
    }
}