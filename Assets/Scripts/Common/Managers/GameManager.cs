using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Common
{
    public class GameManager : BaseSingleton<GameManager>
    {
        public UnityAction OnGamePauseEvent;
        public UnityAction OnGameResumeEvent;
        public UnityAction OnGameStartEvent;
        public UnityAction OnGameEndEvent;

        private GameState currentState;

        private void Start()
        {
            UpdateGameState(GameState.Started);
        }

        public void UpdateGameState(GameState newState)
        {
            currentState = newState;

            switch (currentState)
            {
                case GameState.Paused:
                    PauseGame();
                    break;
                case GameState.Resumed:
                    ResumeGame();
                    break;
                case GameState.Playing:
                    break;
                case GameState.Started:
                    StartGame();
                    break;
                case GameState.Ended:
                    EndGame();
                    break;
            }
        }

        private void EndGame()
        {
            OnGameEndEvent?.Invoke();
        }

        private void StartGame()
        {
            Time.timeScale = 1;
            OnGameStartEvent?.Invoke();
            UpdateGameState(GameState.Playing);
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;
            OnGameResumeEvent?.Invoke();
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            OnGamePauseEvent?.Invoke();
        }

        public void OpenScene(int index)
        {
            SceneManager.LoadScene(index, LoadSceneMode.Single);
        }
        
        public void OpenSceneAdditive(int index)
        {
            SceneManager.LoadScene(index, LoadSceneMode.Additive);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}