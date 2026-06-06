using Asteroids.Core;
using Asteroids.Enemies;
using Asteroids.Infrastructure;
using Zenject;

namespace Asteroids.Gameplay
{
    public class GameOverHandler : IInitializable
    {
        private readonly AsteroidSpawner _asteroidSpawner;
        private readonly UFOSpawner _ufoSpawner;
        private readonly GameStateManager _stateManager;
        private readonly SignalBus _signalBus;
        private readonly IAdProvider _adProvider;

        public GameOverHandler(
            AsteroidSpawner asteroidSpawner,
            UFOSpawner ufoSpawner,
            GameStateManager stateManager,
            SignalBus signalBus,
            IAdProvider adProvider)
        {
            _asteroidSpawner = asteroidSpawner;
            _ufoSpawner = ufoSpawner;
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
            _asteroidSpawner.StopSpawning();
            _ufoSpawner.StopSpawning();
            _adProvider.ShowInterstitial();
        }
    }
}