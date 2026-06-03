using Asteroids.Player;
using Asteroids.Infrastructure;
using UnityEngine;
using Zenject;

namespace Asteroids.Weapons
{
    public class WeaponController : ITickable
    {
        private readonly PlayerModel _player;
        private readonly BulletFactory _bulletFactory;
        private readonly LaserView _laserView;
        private readonly IInputHandler _input;
        private float _fireCooldown;
        private readonly float _fireRate;

        public WeaponController(
            PlayerModel player,
            BulletFactory bulletFactory,
            LaserView laserView,
            IInputHandler input,
            PlayerConfig config)
        {
            _player = player;
            _bulletFactory = bulletFactory;
            _laserView = laserView;
            _input = input;
            _fireRate = config.fireRate;
        }

        public void Tick()
        {
            if (_fireCooldown > 0)
                _fireCooldown -= Time.deltaTime;

            if (_input.Fire && _fireCooldown <= 0)
                Shoot();

            if (_input.Laser)
                _laserView.Fire();
        }

        private void Shoot()
        {
            _fireCooldown = _fireRate;
            float radians = _player.Body.Rotation * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Sin(radians), Mathf.Cos(radians));
            _bulletFactory.Create(_player.Body.Position, direction);
        }
    }
}