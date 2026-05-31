using Asteroids.Infrastructure;
using Asteroids.Player;
using UnityEngine;
using Zenject;

namespace Asteroids.Weapons
{
    public class WeaponController : ITickable
    {
        private readonly PlayerModel _player;
        private readonly ObjectPool<BulletView> _bulletPool;
        private readonly LaserView _laserView;
        private readonly float _bulletSpeed;
        private float _fireCooldown;
        private readonly float _fireRate;

        public WeaponController(PlayerModel player, ObjectPool<BulletView> bulletPool,
            PlayerConfig config, LaserView laserView)
        {
            _player = player;
            _bulletPool = bulletPool;
            _bulletSpeed = config.bulletSpeed;
            _fireRate = config.fireRate;
            _laserView = laserView;
        }

        public void Tick()
        {
            if (_fireCooldown > 0)
                _fireCooldown -= Time.deltaTime;

            if (Input.GetKey(KeyCode.Space) && _fireCooldown <= 0)
                Shoot();

            if (Input.GetKeyDown(KeyCode.E))
                _laserView.Fire();
        }

        private void Shoot()
        {
            _fireCooldown = _fireRate;
            float rad = _player.Body.Rotation * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));

            BulletView view = _bulletPool.Get();
            Bullet bullet = new Bullet(_bulletSpeed, _player.Body.Position, dir);
            view.Init(bullet);
        }
    }
}