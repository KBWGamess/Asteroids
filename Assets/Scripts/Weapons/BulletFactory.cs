using Asteroids.Infrastructure;
using Asteroids.Player;
using UnityEngine;

namespace Asteroids.Weapons
{
    public class BulletFactory
    {
        private readonly ObjectPool<BulletView> _pool;
        private readonly PlayerConfig _config;

        public BulletFactory(ObjectPool<BulletView> pool, PlayerConfig config)
        {
            _pool = pool;
            _config = config;
        }

        public void Create(Vector2 position, Vector2 direction)
        {
            BulletView view = _pool.GetInactive();
            view.transform.position = position;
            Bullet bullet = new Bullet(_config.bulletSpeed, position, direction, _config.bulletLifetime);
            view.Init(bullet);
        }
    }
}