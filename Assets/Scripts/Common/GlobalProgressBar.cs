using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class GlobalProgressBar : BaseSingleton<GlobalProgressBar>
    {
        [SerializeField] private Image imageProgress;
        [SerializeField] private TMP_Text textProgress;
        [SerializeField] private TMP_Text title;

        private Timer timerProgress;
        
        public void ShowProgressBar(string titleBar, Timer timer)
        {
            gameObject.SetActive(true);
            timerProgress = timer;
            if(title != null && titleBar != null)
                title.text = titleBar;
            timerProgress.UpdatedEvent += UpdateProgress;
            timerProgress.EndEvent += () => gameObject.SetActive(false);
            timerProgress.Start();
        }

        private void UpdateProgress(float value)
        {
            if(timerProgress == null)
                throw new NullReferenceException("Timer in GlobalProgressBar is null");

            if(textProgress != null)
                textProgress.text = $"{timerProgress.RemainingTime:#.#}/{timerProgress.Time:#}";
            if(imageProgress != null)
                imageProgress.fillAmount = timerProgress.Normalized;
        }
    }
}