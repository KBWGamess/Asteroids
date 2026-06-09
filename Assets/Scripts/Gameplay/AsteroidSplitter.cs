using Asteroids.Core;
using Asteroids.Enemies;
using Asteroids.Infrastructure;
using UnityEngine;

namespace Asteroids.Gameplay
{
    public class AsteroidSplitter
    {
        private readonly IEnemyFactory _factory;
        private readonly EnemyConfig _config;

        public AsteroidSplitter(IEnemyFactory factory, EnemyConfig config)
        {
            _factory = factory;
            _config = config;
        }

        public void Split(AsteroidView asteroid)
        {
            if (asteroid.Model.Size != AsteroidSize.Small)
            {
                EnemySize newSize = asteroid.Model.Size == AsteroidSize.Large
                    ? EnemySize.Medium
                    : EnemySize.Small;

                float newSpeed = asteroid.Model.Size == AsteroidSize.Large
                    ? _config.asteroidMediumSpeed
                    : _config.asteroidSmallSpeed;

                for (int i = 0; i < _config.asteroidSplitCount; i++)
                {
                    _factory.CreateAsteroid(
                        asteroid.Model.Body.Position, 
                        CreateRandomDirection(), 
                        newSpeed, 
                        newSize);
                }
            }

            asteroid.Deactivate();
        }

        private Vector2 CreateRandomDirection()
        {
            return new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized;
        }
    }
}