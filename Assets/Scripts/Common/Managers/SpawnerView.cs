using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    [System.Serializable]
    public class SpawnerView
    {
        [SerializeField] private Image waveReloadProgress;
        [SerializeField] private TMP_Text waveReloadTime;
        [SerializeField] private TMP_Text waveCount;

        private SpawnerManager spawnerManager;
        private float maxTime;

        public void Start()
        {
            spawnerManager = SpawnerManager.Instance;
            maxTime = spawnerManager.WaveTimer.Time;
            spawnerManager.UpdateWaveEvent += UpdateWaveCount;
            spawnerManager.WaveTimer.OnUpdatedEvent += UpdateReloadWave;
            spawnerManager.WaveTimer.OnStartEvent += StartWave;
            spawnerManager.WaveTimer.OnEndEvent += EndWave;
        }

        private void StartWave()
        {
            if(waveReloadProgress != null)
                waveReloadProgress.gameObject.SetActive(true);
            if(waveReloadTime != null)
                waveReloadTime.gameObject.SetActive(true);
        }

        private void UpdateReloadWave(float time)
        {
            if(waveReloadProgress != null)
                waveReloadProgress.fillAmount = time / maxTime;

            if(waveReloadTime != null)
                waveReloadTime.text = $"Next Wave Loading:{maxTime:#.#}/{time:#.#}";
        }

        private void UpdateWaveCount(int wave)
        {
            waveCount.text = $"Wave: {wave}";
        }

        private void EndWave()
        {
            if(waveReloadProgress != null)
                waveReloadProgress.gameObject.SetActive(false);
            if(waveReloadTime != null)
                waveReloadTime.gameObject.SetActive(false);
        }
    }
}