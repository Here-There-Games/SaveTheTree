using TMPro;
using UnityEngine;

namespace Common
{
    [System.Serializable]
    public class SpawnerView
    {
        [SerializeField] private TMP_Text waveReload;
        [SerializeField] private TMP_Text waveCount;

        private SpawnerManager spawnerManager;
        private float currentWave;

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
                waveReload.text = $"Wave | {currentWave + 1}"; 
        }

        private void UpdateReloadWave(float time)
        {
            Color waveColor = waveReload.color;
            waveReload.color = new Color(waveColor.r, waveColor.g, waveColor.b, time);
        }

        private void UpdateWaveCount(int wave)
        {
            currentWave = wave;
            UpdateWaveView();
        }

        private void UpdateWaveView()
        {
            waveCount.text = $"Wave: {currentWave}";
        }
    }
}