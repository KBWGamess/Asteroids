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
        private readonly EnemyRegistry _registry;

        public EnemyFactory(
            ObjectPool<AsteroidView> asteroidPool,
            ObjectPool<UFOView> ufoPool,
            EnemyRegistry registry)
        {
            _asteroidPool = asteroidPool;
            _ufoPool = ufoPool;
            _registry = registry;
        }

        public void CreateAsteroid(Vector2 position, Vector2 direction, float speed, EnemySize size = EnemySize.Large)
        {
            AsteroidView view = _asteroidPool.Get();
            AsteroidSize asteroidSize = size == EnemySize.Large ? AsteroidSize.Large :
                size == EnemySize.Medium ? AsteroidSize.Medium :
                AsteroidSize.Small;
            AsteroidModel model = new AsteroidModel(position, direction, speed, asteroidSize);
            view.transform.position = position;
            view.SetupModel(model);
            _registry.Register(view);
        }

        public void CreateUFO(Vector2 position, float speed)
        {
            UFOView view = _ufoPool.Get();
            UFOModel model = new UFOModel(position, speed);
            view.transform.position = position;
            view.SetupModel(model);
            _registry.Register(view);
        }
    }
}