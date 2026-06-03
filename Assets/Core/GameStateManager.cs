namespace Asteroids.Core
{
    public enum GameStateType { Playing, GameOver, Paused }

    public class GameStateManager
    {
        public GameStateType CurrentState { get; private set; } = GameStateType.Playing;

        public void SetGameOver() => CurrentState = GameStateType.GameOver;
        public void SetPaused() => CurrentState = GameStateType.Paused;
        public void SetPlaying() => CurrentState = GameStateType.Playing;
    }
}