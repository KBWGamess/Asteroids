using System;

namespace Asteroids.Infrastructure
{
    [Serializable]
    public class EnemyConfig
    {
        public float asteroidSpeed = 3f;
        public float ufoSpeed = 2f;
        public int maxEnemiesOnMap = 10;
        public float spawnInterval = 2f;
        
        public float asteroidRadiusLarge = 0.7f;
        public float asteroidRadiusMedium = 0.4f;
        public float asteroidRadiusSmall = 0.2f;
        public float ufoRadius = 0.25f;
        public float knockbackForce = 3f;
        
        public int rewardAsteroidLarge = 20;
        public int rewardAsteroidMedium = 50;
        public int rewardAsteroidSmall = 100;
        public int rewardUfo = 200;
    }
}