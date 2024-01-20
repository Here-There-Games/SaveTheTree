using System;
using Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GlobalProgressBar : BaseSingleton<GlobalProgressBar>
    {
        [SerializeField] private Image imageProgress;
        [SerializeField] private TMP_Text textProgress;
        [SerializeField] private TMP_Text title;

        private Timer timerProgress;
        private CanvasGroup canvasGroup;

        public void ShowProgressBar(string titleBar, Timer timer)
        {
            Visible(true);
            timerProgress = new Timer(this,timer);
            if(title != null && titleBar != null)
                title.text = titleBar;
            timerProgress.OnUpdatedNormalizedEvent += UpdateProgress;
            timerProgress.OnEndEvent += () => Visible(false);
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

        protected override void Initialize()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            Visible(false);
        }

        private void Visible(bool visible)
        {
            canvasGroup.alpha = visible ? 1 : 0;
            canvasGroup.blocksRaycasts = visible;
        }
    }
}