using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Common.Managers
{
    public class GameManager : BaseSingleton<GameManager>
    {
        public UnityAction OnGamePauseEvent;
        public UnityAction OnGameResumeEvent;
        public UnityAction OnGameStartEvent;
        public UnityAction OnGameEndEvent;

        private GameState currentState;
        private InputManager inputManager;
        private InputAction gamePause;

        private void Start()
        {
            UpdateGameState(GameState.Started);
            gamePause = InputManager.Instance.InputControl.Player.Escape;

            gamePause.started += _ =>
                                     {
                                         UpdateGameState(currentState == GameState.Paused
                                                             ? GameState.Resumed
                                                             : GameState.Paused);
                                     };
            inputManager = InputManager.Instance;
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
            inputManager.InputControl.Player.Enable();
            inputManager.InputControl.UI.Enable();
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            OnGamePauseEvent?.Invoke();
            inputManager.InputControl.Player.Disable();
            inputManager.InputControl.UI.Disable();
        }

        public void OpenScene(int index, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(index, mode);
        }
        
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}