using System;
using System.Runtime.CompilerServices;
using Common.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class MenuManager : BaseSingleton<MenuManager>
    {
        [SerializeField] private CanvasGroup pause;
        [SerializeField] private CanvasGroup gameOver;

        protected override void Initialize()
        {
            if(pause != null)
                Visible(pause, false);
            if(gameOver != null)
                Visible(gameOver, false);
        }

        public void Pause()
        {
            if(pause == null)
                throw new NullReferenceException();
            Visible(pause, true);
            Time.timeScale = 0;
        }

        public void BackOnGame()
        {
            if(pause == null)
                throw new NullReferenceException();
            Visible(pause, false);
            Time.timeScale = 1;
        }

        public void GameOver()
        {
            if(gameOver == null)
                throw new NullReferenceException();
            Visible(gameOver, true);
            Time.timeScale = 0;
        }

        private void Visible(CanvasGroup panel, bool visible)
        {
            panel.alpha = visible ? 1 : 0;
            panel.blocksRaycasts = visible;
            panel.interactable = visible;
        }

        public void OpenScene(int index)
        {
            if(Time.timeScale == 0)
                Time.timeScale = 1;
            Loader.LoadScene(index);
        }
    }
}