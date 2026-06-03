using System;
using System.Collections.Generic;
using Asteroids.Enemies;
using Asteroids.Infrastructure;
using Asteroids.Core;
using Asteroids.Player;
using Asteroids.Weapons;
using UnityEngine;
using Zenject;

namespace Asteroids.Gameplay
{
    public class EnemyCollisionHandler : ITickable
    {
        private readonly PlayerModel _player;
        private readonly PlayerView _playerView;
        private readonly ObjectPool<AsteroidView> _asteroidPool;
        private readonly ObjectPool<BulletView> _bulletPool;
        private readonly ObjectPool<UFOView> _ufoPool;
        private readonly RewardService _rewardService;
        private readonly AsteroidSplitter _splitter;
        private readonly LaserView _laserView;
        private readonly SignalBus _signalBus;
        private readonly float _playerRadius;
        private readonly float _bulletRadius;
        private readonly float _ufoRadius;
        private readonly float _knockbackForce;
        private readonly EnemyConfig _enemyConfig;
        private readonly PlayerConfig _playerConfig;

        public EnemyCollisionHandler(
            PlayerModel player,
            PlayerView playerView,
            ObjectPool<AsteroidView> asteroidPool,
            ObjectPool<BulletView> bulletPool,
            ObjectPool<UFOView> ufoPool,
            RewardService rewardService,
            AsteroidSplitter splitter,
            LaserView laserView,
            SignalBus signalBus,
            PlayerConfig playerConfig,
            EnemyConfig enemyConfig)
        {
            _player = player;
            _playerView = playerView;
            _asteroidPool = asteroidPool;
            _bulletPool = bulletPool;
            _ufoPool = ufoPool;
            _rewardService = rewardService;
            _splitter = splitter;
            _laserView = laserView;
            _signalBus = signalBus;
            _playerRadius = playerConfig.playerRadius;
            _bulletRadius = playerConfig.bulletRadius;
            _ufoRadius = enemyConfig.ufoRadius;
            _knockbackForce = enemyConfig.knockbackForce;
            _enemyConfig = enemyConfig;
            _playerConfig = playerConfig;
        }

        public void Tick()
        {
            CheckAsteroidCollisions();
            CheckUFOCollisions();
            CheckLaserCollisions();
        }

        private void CheckAsteroidCollisions()
        {
            CheckEnemyCollisions(
                _asteroidPool.GetActive(),
                a => a.Model != null && a.Model.IsAlive,
                a => (Vector2)a.transform.position,
                a => GetAsteroidRadius(a.Model.Size),
                a => { _rewardService.GiveReward(a.Model.Size); _splitter.Split(a); },
                a =>
                {
                    _player.TakeDamage();
                    ApplyKnockback(a.Model.Body.Position);
                    _signalBus.Fire(new OnPlayerHit { RemainingHealth = _player.Health });
                    if (_player.IsDead()) _signalBus.Fire(new OnPlayerDied());
                }
            );
        }

        private void CheckUFOCollisions()
        {
            CheckEnemyCollisions(
                _ufoPool.GetActive(),
                u => u.Model != null && u.Model.IsAlive,
                u => (Vector2)u.transform.position,
                u => _ufoRadius,
                u => { _rewardService.GiveReward(EnemyType.UFO); u.Deactivate(); },
                u =>
                {
                    _player.TakeDamage();
                    ApplyKnockback(u.Model.Body.Position);
                    _signalBus.Fire(new OnPlayerHit { RemainingHealth = _player.Health });
                    if (_player.IsDead()) _signalBus.Fire(new OnPlayerDied());
                }
            );
        }

        private void CheckEnemyCollisions<T>(
            IReadOnlyList<T> enemies,
            Func<T, bool> isAlive,
            Func<T, Vector2> getPosition,
            Func<T, float> getRadius,
            Action<T> onBulletHit,
            Action<T> onPlayerHit)
            where T : MonoBehaviour
        {
            foreach (var enemy in enemies)
            {
                if (!isAlive(enemy)) continue;

                float radius = getRadius(enemy);

                foreach (var bullet in _bulletPool.GetActive())
                {
                    if (CircleOverlap(bullet.transform.position, _bulletRadius,
                        enemy.transform.position, radius))
                    {
                        bullet.Deactivate();
                        onBulletHit(enemy);
                        break;
                    }
                }

                if (!enemy.gameObject.activeSelf) continue;

                if (!_player.IsDead() && CircleOverlap(_playerView.transform.position, _playerRadius,
                    enemy.transform.position, radius))
                {
                    onPlayerHit(enemy);
                }
            }
        }

        private void CheckLaserCollisions()
        {
            if (!_laserView.IsActive) return;
            if (_playerConfig == null) return;

            foreach (var asteroid in _asteroidPool.GetActive())
            {
                if (asteroid.Model == null || !asteroid.Model.IsAlive) continue;
                if (IsOnLaserLine(asteroid.transform.position, _playerConfig.laserRadius))
                {
                    _rewardService.GiveReward(asteroid.Model.Size);
                    asteroid.Deactivate();
                }
            }

            foreach (var ufo in _ufoPool.GetActive())
            {
                if (ufo.Model == null || !ufo.Model.IsAlive) continue;
                if (IsOnLaserLine(ufo.transform.position, _ufoRadius))
                {
                    _rewardService.GiveReward(EnemyType.UFO);
                    ufo.Deactivate();
                }
            }
        }

        private float GetAsteroidRadius(AsteroidSize size) =>
            size == AsteroidSize.Large ? _enemyConfig.asteroidRadiusLarge :
            size == AsteroidSize.Medium ? _enemyConfig.asteroidRadiusMedium :
            _enemyConfig.asteroidRadiusSmall;

        private bool CircleOverlap(Vector2 a, float ra, Vector2 b, float rb) =>
            (a - b).sqrMagnitude < (ra + rb) * (ra + rb);

        private bool IsOnLaserLine(Vector2 point, float radius)
        {
            Vector2 origin = _laserView.Origin;
            Vector2 direction = _laserView.Direction;
            Vector2 toPoint = point - origin;
            float dot = Vector2.Dot(toPoint, direction);
            if (dot < 0) return false;
            Vector2 closest = origin + direction * dot;
            return (closest - point).magnitude < radius;
        }

        private void ApplyKnockback(Vector2 enemyPosition)
        {
            Vector2 direction = (enemyPosition - _player.Body.Position).normalized;
            _player.Body.SetVelocity(-direction * _knockbackForce);
        }
    }
}