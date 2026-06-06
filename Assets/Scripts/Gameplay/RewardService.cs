using Asteroids.Enemies;
using Asteroids.Core;
using Zenject;

namespace Asteroids.Gameplay
{
    public class RewardService
    {
        private readonly ScoreService _scoreService;
        private readonly SignalBus _signalBus;

        public RewardService(ScoreService scoreService, SignalBus signalBus)
        {
            _scoreService = scoreService;
            _signalBus = signalBus;
        }

        public void GiveReward(AsteroidSize size)
        {
            EnemyType type = size == AsteroidSize.Large ? EnemyType.AsteroidLarge :
                size == AsteroidSize.Medium ? EnemyType.AsteroidMedium :
                EnemyType.AsteroidSmall;
            _scoreService.AddScore(type);
            _signalBus.Fire(new Asteroids.Core.OnEnemyKilled { Score = _scoreService.Score });
        }

        public void GiveReward(EnemyType type)
        {
            _scoreService.AddScore(type);
            _signalBus.Fire(new Asteroids.Core.OnEnemyKilled { Score = _scoreService.Score });
        }
    }
}