namespace Asteroids.Core
{
    public enum GameStateType { Playing, GameOver, Paused }

    public abstract class GameState
    {
        public abstract GameStateType Type { get; }
        public abstract void Enter();
        public abstract void Exit();
    }

    public class PlayingState : GameState
    {
        public override GameStateType Type => GameStateType.Playing;
        public override void Enter() { }
        public override void Exit() { }
    }

    public class GameOverState : GameState
    {
        public override GameStateType Type => GameStateType.GameOver;
        public override void Enter() { }
        public override void Exit() { }
    }

    public class PausedState : GameState
    {
        public override GameStateType Type => GameStateType.Paused;
        public override void Enter() { }
        public override void Exit() { }
    }
}