using Zenject;

namespace Asteroids.Core
{
    public class GameStateMachine
    {
        private GameState _currentState;
        private readonly SignalBus _signalBus;

        public GameStateType CurrentState => _currentState.Type;

        public GameStateMachine(SignalBus signalBus)
        {
            _signalBus = signalBus;
            ChangeState(new PlayingState());
        }

        public void ChangeState(GameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void SetGameOver() => ChangeState(new GameOverState());
        public void SetPaused() => ChangeState(new PausedState());
        public void SetPlaying() => ChangeState(new PlayingState());
    }
}