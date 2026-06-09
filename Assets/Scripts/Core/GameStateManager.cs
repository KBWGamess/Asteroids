using UnityEngine;

namespace Asteroids.Core
{
    public enum GameStateType { Playing, GameOver, Paused }

    public class GameStateManager
    {
        public GameStateType CurrentState { get; private set; } = GameStateType.Playing;
        public bool IsPlaying => CurrentState == GameStateType.Playing;

        public void SetGameOver()
        {
            CurrentState = GameStateType.GameOver;
        }

        public void SetPaused()
        {
            CurrentState = GameStateType.Paused;
            Time.timeScale = 0f;
        }

        public void SetPlaying()
        {
            CurrentState = GameStateType.Playing;
            Time.timeScale = 1f;
        }
    }
}