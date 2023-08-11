using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class MenuManager : BaseSingleton<MenuManager>
    {
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject gameOverPanel;

        public void Pause()
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }

        public void BackOnGame()
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }

        public void GameOver()
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
        
        public void OpenScene(int index)
        {
            SceneManager.LoadScene(index, LoadSceneMode.Single);
        }
    }
}