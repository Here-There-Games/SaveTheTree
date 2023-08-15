using System;
using UnityEngine;

namespace Common
{
    public class GameManager : BaseSingleton<GameManager>
    {
        public event Action OnGamePause;
        public event Action OnGameResume;
        public event Action OnGameStart;
        public event Action OnGameEndEvent;

        private GameState state;

        private void Start()
        {
            UpdateGameState(GameState.Started);
        }

        public void UpdateGameState(GameState newState)
        {
            state = newState;

            switch (state)
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
            Debug.Log("gamestart");
            Time.timeScale = 1.0f;
            OnGameStart?.Invoke();
            UpdateGameState(GameState.Playing);
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;
            OnGameResume?.Invoke();
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            OnGamePause?.Invoke();
        }
    }
}