using Asteroids.Core;
using Asteroids.Infrastructure;
using Zenject;

namespace Asteroids.Gameplay
{
    public class GameAnalyticsHandler : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly GameFacade _facade;
        private readonly IAnalyticsService _analytics;

        public GameAnalyticsHandler(IAnalyticsService analytics, SignalBus signalBus, GameFacade facade)
        {
            _analytics = analytics;
            _signalBus = signalBus;
            _facade = facade;
        }

        public void Initialize()
        {
            _analytics.Initialize();
            _analytics.LogGameStart();
            _signalBus.Subscribe<OnPlayerDied>(OnPlayerDied);
        }

        private void OnPlayerDied()
        {
            _analytics.LogGameOver(_facade.GetScore());
        }
    }
}