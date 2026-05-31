using Asteroids.Core;
using Asteroids.Enemies;
using Asteroids.Infrastructure;
using UnityEngine;

namespace Asteroids.Gameplay
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly ObjectPool<AsteroidView> _asteroidPool;
        private readonly ObjectPool<UfoView> _ufoPool;
        private readonly WorldBounds _bounds;
        private readonly EnemyRegistry _registry;

        public EnemyFactory(
            ObjectPool<AsteroidView> asteroidPool,
            ObjectPool<UfoView> ufoPool,
            WorldBounds bounds,
            EnemyRegistry registry)
        {
            _asteroidPool = asteroidPool;
            _ufoPool = ufoPool;
            _bounds = bounds;
            _registry = registry;
        }

        public void CreateAsteroid(Vector2 position, Vector2 direction, float speed)
        {
            AsteroidView view = _asteroidPool.Get();
            AsteroidModel model = new AsteroidModel(position, direction, speed, AsteroidSize.Large);
            view.Init(model, _bounds);
            _registry.Register(view.gameObject);
        }

        public void CreateUfo(Vector2 position, float speed)
        {
            UfoView view = _ufoPool.Get();
            UfoModel model = new UfoModel(position, speed);
            view.Init(model, _bounds);
            _registry.Register(view.gameObject);
        }
    }
}