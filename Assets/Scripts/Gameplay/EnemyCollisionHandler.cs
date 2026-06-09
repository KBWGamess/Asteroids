using System;
using System.Collections.Generic;
using Asteroids.Enemies;
using Asteroids.Infrastructure;
using Asteroids.Player;
using Asteroids.Weapons;
using UnityEngine;
using Zenject;
using Asteroids.Core;

namespace Asteroids.Gameplay
{
    public class EnemyCollisionHandler : ITickable
    {
        private readonly PlayerView _playerView;
        private readonly ObjectPool<AsteroidView> _asteroidPool;
        private readonly ObjectPool<BulletView> _bulletPool;
        private readonly ObjectPool<UFOView> _ufoPool;
        private readonly LaserView _laserView;
        private readonly CollisionReactionHandler _reactionHandler;
        private readonly float _playerRadius;
        private readonly float _bulletRadius;
        private readonly float _ufoRadius;
        private readonly EnemyConfig _enemyConfig;
        private readonly PlayerConfig _playerConfig;
        private readonly GameStateManager _stateManager;

        public EnemyCollisionHandler(
            PlayerView playerView,
            ObjectPool<AsteroidView> asteroidPool,
            ObjectPool<BulletView> bulletPool,
            ObjectPool<UFOView> ufoPool,
            LaserView laserView,
            CollisionReactionHandler reactionHandler,
            PlayerConfig playerConfig,
            EnemyConfig enemyConfig,
            GameStateManager stateManager)
        {
            _playerView = playerView;
            _asteroidPool = asteroidPool;
            _bulletPool = bulletPool;
            _ufoPool = ufoPool;
            _laserView = laserView;
            _reactionHandler = reactionHandler;
            _playerRadius = playerConfig.playerRadius;
            _bulletRadius = playerConfig.bulletRadius;
            _ufoRadius = enemyConfig.ufoRadius;
            _enemyConfig = enemyConfig;
            _playerConfig = playerConfig;
            _stateManager = stateManager;
        }

        public void Tick()
        {
            if (!_stateManager.IsPlaying) return;
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
                a => _reactionHandler.OnBulletHitAsteroid(a),
                a => _reactionHandler.OnPlayerHitAsteroid(a)
            );
        }

        private void CheckUFOCollisions()
        {
            CheckEnemyCollisions(
                _ufoPool.GetActive(),
                u => u.Model != null && u.Model.IsAlive,
                u => (Vector2)u.transform.position,
                u => _ufoRadius,
                u => _reactionHandler.OnBulletHitUFO(u),
                u => _reactionHandler.OnPlayerHitUFO(u)
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

                if (CircleOverlap(_playerView.transform.position, _playerRadius,
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

            CheckLaserForPool(_asteroidPool.GetActive(),
                a => a.Model != null && a.Model.IsAlive,
                a => a.transform.position,
                a => _playerConfig.laserRadius,
                a => _reactionHandler.OnLaserHitAsteroid(a));

            CheckLaserForPool(_ufoPool.GetActive(),
                u => u.Model != null && u.Model.IsAlive,
                u => u.transform.position,
                u => _ufoRadius,
                u => _reactionHandler.OnLaserHitUFO(u));
        }

        private void CheckLaserForPool<T>(
            IReadOnlyList<T> enemies,
            Func<T, bool> isAlive,
            Func<T, Vector3> getPosition,
            Func<T, float> getRadius,
            Action<T> onHit)
            where T : MonoBehaviour
        {
            foreach (var enemy in enemies)
            {
                if (!isAlive(enemy)) continue;
                if (IsOnLaserLine(getPosition(enemy), getRadius(enemy)))
                    onHit(enemy);
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
    }
}