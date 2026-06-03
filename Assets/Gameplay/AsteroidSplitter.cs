using Asteroids.Core;
using Asteroids.Enemies;
using Asteroids.Infrastructure;
using UnityEngine;

namespace Asteroids.Gameplay
{
    public class AsteroidSplitter
    {
        private readonly IEnemyFactory _factory;

        public AsteroidSplitter(IEnemyFactory factory)
        {
            _factory = factory;
        }

        public void Split(AsteroidView asteroid)
        {
            if (asteroid.Model.Size != AsteroidSize.Small)
            {
                EnemySize newSize = asteroid.Model.Size == AsteroidSize.Large
                    ? EnemySize.Medium
                    : EnemySize.Small;

                float newSpeed = asteroid.Model.Size == AsteroidSize.Large ? 4f : 6f;

                for (int i = 0; i < 2; i++)
                {
                    Vector2 direction = new Vector2(
                        Random.Range(-1f, 1f),
                        Random.Range(-1f, 1f)
                    ).normalized;

                    _factory.CreateAsteroid(asteroid.Model.Body.Position, direction, newSpeed, newSize);
                }
            }

            asteroid.Deactivate();
        }
    }
}