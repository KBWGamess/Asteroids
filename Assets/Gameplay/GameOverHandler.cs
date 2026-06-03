using Asteroids.Core;
using Asteroids.Infrastructure;
using Zenject;

namespace Asteroids.Gameplay
{
    public class GameOverHandler : IInitializable
    {
        private readonly GameFacade _facade;
        private readonly GameStateManager _stateManager;
        private readonly SignalBus _signalBus;
        private readonly IAdProvider _adProvider;

        public GameOverHandler(
            GameFacade facade,
            GameStateManager stateManager,
            SignalBus signalBus,
            IAdProvider adProvider)
        {
            _facade = facade;
            _stateManager = stateManager;
            _signalBus = signalBus;
            _adProvider = adProvider;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<OnPlayerDied>(OnPlayerDied);
        }

        private void OnPlayerDied()
        {
            _stateManager.SetGameOver();
            _facade.StopGame();
            _adProvider.ShowInterstitial();
        }
    }
}