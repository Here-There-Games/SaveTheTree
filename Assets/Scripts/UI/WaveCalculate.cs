using System;
using Common;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WaveCalculate : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        private void Start()
        {
            SpawnerManager.Instance.UpdateWaveEvent += UpdateWave;
        }

        private void UpdateWave(int wave)
        {
            text.text = $"Wave: {wave}";
        }
    }
}