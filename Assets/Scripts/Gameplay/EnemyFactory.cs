using Asteroids.Core;
using Asteroids.Enemies;
using Asteroids.Infrastructure;
using UnityEngine;

namespace Asteroids.Gameplay
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly ObjectPool<AsteroidView> _asteroidPool;
        private readonly ObjectPool<UFOView> _ufoPool;

        public EnemyFactory(
            ObjectPool<AsteroidView> asteroidPool,
            ObjectPool<UFOView> ufoPool)
        {
            _asteroidPool = asteroidPool;
            _ufoPool = ufoPool;
        }

        public void CreateAsteroid(Vector2 position, Vector2 direction, float speed, EnemySize size = EnemySize.Large)
        {
            AsteroidView view = _asteroidPool.GetInactive();
            AsteroidSize asteroidSize = size == EnemySize.Large ? AsteroidSize.Large :
                size == EnemySize.Medium ? AsteroidSize.Medium :
                AsteroidSize.Small;
            AsteroidModel model = new AsteroidModel(position, direction, speed, asteroidSize);
            view.transform.position = position;
            view.SetupModel(model);
        }

        public void CreateUFO(Vector2 position, float speed)
        {
            UFOView view = _ufoPool.GetInactive();
            UFOModel model = new UFOModel(position, speed);
            view.transform.position = position;
            view.SetupModel(model);
        }
    }
}