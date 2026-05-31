using System.Collections.Generic;
using Asteroids.Enemies;

namespace Asteroids.Gameplay
{
    public enum EnemyType { AsteroidLarge, AsteroidMedium, AsteroidSmall, Ufo }

    public class ScoreSystem
    {
        private int _score;
        public int Score => _score;

        private readonly Dictionary<EnemyType, int> _rewards = new Dictionary<EnemyType, int>
        {
            { EnemyType.AsteroidLarge, 20 },
            { EnemyType.AsteroidMedium, 50 },
            { EnemyType.AsteroidSmall, 100 },
            { EnemyType.Ufo, 200 }
        };

        public void AddScore(EnemyType type)
        {
            _score += _rewards[type];
        }

        public void Reset() => _score = 0;
    }
}