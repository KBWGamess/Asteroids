using System.Collections.Generic;
using Asteroids.Infrastructure;
using Asteroids.Core;

namespace Asteroids.Gameplay
{
    public class ScoreService
    {
        private int _score;
        public int Score => _score;

        private readonly Dictionary<EnemyType, int> _rewards;

        public ScoreService(EnemyConfig config)
        {
            _rewards = new Dictionary<EnemyType, int>
            {
                { EnemyType.AsteroidLarge, config.rewardAsteroidLarge },
                { EnemyType.AsteroidMedium, config.rewardAsteroidMedium },
                { EnemyType.AsteroidSmall, config.rewardAsteroidSmall },
                { EnemyType.UFO, config.rewardUfo }
            };
        }

        public void AddScore(EnemyType type)
        {
            _score += _rewards[type];
        }

        public void Reset() => _score = 0;
    }
}