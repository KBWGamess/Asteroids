using System;

namespace Asteroids.Infrastructure
{
    [Serializable]
    public class EnemyConfig
    {
        public float asteroidSpeed = 3f;
        public float ufoSpeed = 2f;
        public int maxEnemiesOnMap = 15;
        public float spawnInterval = 3f;
    }
}