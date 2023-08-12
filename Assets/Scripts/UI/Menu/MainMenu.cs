using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu
{
    public class MainMenu : BaseSingleton<MainMenu>
    {
        [Header("Buttons")]
        [SerializeField] private Outline playButton;
        [SerializeField] private Outline settingButton;
        [SerializeField] private Outline exitButton;

        [Header("Panels")]
        [SerializeField] private GameObject settingPanel;

        private void Start()
        {
            playButton.ClickEvent += () => SceneManager.LoadScene(1);
            settingButton.ClickEvent += () => settingPanel.SetActive(true);
            exitButton.ClickEvent += Application.Quit;
        }
    }
}