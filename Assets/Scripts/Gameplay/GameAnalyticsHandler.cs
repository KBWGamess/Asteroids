using Asteroids.Core;
using Asteroids.Infrastructure;
using System;
using Zenject;

namespace Asteroids.Gameplay
{
    public class GameAnalyticsHandler : IInitializable, IDisposable
    {
        private readonly IAnalyticsService _analytics;
        private readonly SignalBus _signalBus;
        private readonly ScoreService _scoreService;

        public GameAnalyticsHandler(
            IAnalyticsService analytics,
            SignalBus signalBus,
            ScoreService scoreService)
        {
            _analytics = analytics;
            _signalBus = signalBus;
            _scoreService = scoreService;
        }

        public void Initialize()
        {
            _analytics.Initialize();
            _analytics.LogGameStart();
            _signalBus.Subscribe<OnPlayerDied>(OnPlayerDied);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<OnPlayerDied>(OnPlayerDied);
        }

        private void OnPlayerDied()
        {
            _analytics.LogGameOver(_scoreService.Score);
        }
    }
}