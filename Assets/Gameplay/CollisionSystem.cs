using Asteroids.Enemies;
using Asteroids.Infrastructure;
using Asteroids.Player;
using Asteroids.Weapons;
using Asteroids.Core;
using UnityEngine;
using Zenject;

namespace Asteroids.Gameplay
{
    public class CollisionSystem : ITickable
    {
        private readonly PlayerModel _player;
        private readonly PlayerView _playerView;
        private readonly ObjectPool<AsteroidView> _asteroidPool;
        private readonly ObjectPool<BulletView> _bulletPool;
        private readonly ObjectPool<UfoView> _ufoPool;
        private readonly WorldBounds _bounds;
        private readonly ScoreSystem _scoreSystem;
        private readonly LaserView _laserView;
        private readonly SignalBus _signalBus;
        private readonly float _playerRadius = 0.3f;
        private readonly float _bulletRadius = 0.05f;

        public CollisionSystem(
            PlayerModel player,
            PlayerView playerView,
            ObjectPool<AsteroidView> asteroidPool,
            ObjectPool<BulletView> bulletPool,
            ObjectPool<UfoView> ufoPool,
            WorldBounds bounds,
            ScoreSystem scoreSystem,
            LaserView laserView,
            SignalBus signalBus)
        {
            _player = player;
            _playerView = playerView;
            _asteroidPool = asteroidPool;
            _bulletPool = bulletPool;
            _ufoPool = ufoPool;
            _bounds = bounds;
            _scoreSystem = scoreSystem;
            _laserView = laserView;
            _signalBus = signalBus;
        }

        public void Tick()
        {
            foreach (var asteroid in _asteroidPool.GetActive())
            {
                if (asteroid.Model == null || !asteroid.Model.IsAlive) continue;

                float asteroidRadius = asteroid.Model.Size == AsteroidSize.Large ? 0.7f :
                                       asteroid.Model.Size == AsteroidSize.Medium ? 0.4f : 0.2f;

                foreach (var bullet in _bulletPool.GetActive())
                {
                    if (CircleOverlap(bullet.transform.position, _bulletRadius,
                        asteroid.transform.position, asteroidRadius))
                    {
                        bullet.Deactivate();
                        AddScoreForAsteroid(asteroid.Model.Size);
                        SplitAsteroid(asteroid);
                        break;
                    }
                }

                if (!asteroid.gameObject.activeSelf) continue;

                if (!_player.IsDead() && CircleOverlap(_playerView.transform.position, _playerRadius,
                    asteroid.transform.position, asteroidRadius))
                {
                    _player.TakeDamage();
                    ApplyKnockback(asteroid.Model.Body.Position);
                    _signalBus.Fire(new OnPlayerHit { RemainingHealth = _player.Health });
                    if (_player.IsDead())
                        _signalBus.Fire(new OnPlayerDied());
                }
            }

            foreach (var ufo in _ufoPool.GetActive())
            {
                if (ufo.Model == null || !ufo.Model.IsAlive) continue;

                float ufoRadius = 0.25f;

                foreach (var bullet in _bulletPool.GetActive())
                {
                    if (CircleOverlap(bullet.transform.position, _bulletRadius,
                        ufo.transform.position, ufoRadius))
                    {
                        bullet.Deactivate();
                        _scoreSystem.AddScore(EnemyType.Ufo);
                        _signalBus.Fire(new OnEnemyKilled { Score = _scoreSystem.Score });
                        ufo.Deactivate();
                        break;
                    }
                }

                if (!ufo.gameObject.activeSelf) continue;

                if (!_player.IsDead() && CircleOverlap(_playerView.transform.position, _playerRadius,
                    ufo.transform.position, ufoRadius))
                {
                    _player.TakeDamage();
                    ApplyKnockback(ufo.Model.Body.Position);
                    _signalBus.Fire(new OnPlayerHit { RemainingHealth = _player.Health });
                    if (_player.IsDead())
                        _signalBus.Fire(new OnPlayerDied());
                }
            }

            if (_laserView.IsActive)
            {
                foreach (var asteroid in _asteroidPool.GetActive())
                {
                    if (asteroid.Model == null || !asteroid.Model.IsAlive) continue;
                    if (IsOnLaserLine(asteroid.transform.position, 0.5f))
                    {
                        AddScoreForAsteroid(asteroid.Model.Size);
                        asteroid.Deactivate();
                    }
                }

                foreach (var ufo in _ufoPool.GetActive())
                {
                    if (ufo.Model == null || !ufo.Model.IsAlive) continue;
                    if (IsOnLaserLine(ufo.transform.position, 0.25f))
                    {
                        _scoreSystem.AddScore(EnemyType.Ufo);
                        _signalBus.Fire(new OnEnemyKilled { Score = _scoreSystem.Score });
                        ufo.Deactivate();
                    }
                }
            }
        }

        private void AddScoreForAsteroid(AsteroidSize size)
        {
            EnemyType type = size == AsteroidSize.Large ? EnemyType.AsteroidLarge :
                             size == AsteroidSize.Medium ? EnemyType.AsteroidMedium :
                             EnemyType.AsteroidSmall;
            _scoreSystem.AddScore(type);
            _signalBus.Fire(new OnEnemyKilled { Score = _scoreSystem.Score });
        }

        private void SplitAsteroid(AsteroidView asteroid)
        {
            if (asteroid.Model.Size != AsteroidSize.Small)
            {
                AsteroidSize newSize = asteroid.Model.Size == AsteroidSize.Large
                    ? AsteroidSize.Medium
                    : AsteroidSize.Small;

                float newSpeed = asteroid.Model.Size == AsteroidSize.Large ? 4f : 6f;

                for (int i = 0; i < 2; i++)
                {
                    Vector2 dir = new Vector2(
                        Random.Range(-1f, 1f),
                        Random.Range(-1f, 1f)
                    ).normalized;

                    AsteroidView fragment = _asteroidPool.Get();
                    AsteroidModel model = new AsteroidModel(
                        asteroid.Model.Body.Position, dir, newSpeed, newSize);
                    fragment.Init(model, _bounds);
                }
            }

            asteroid.Deactivate();
        }

        private bool CircleOverlap(Vector2 a, float ra, Vector2 b, float rb)
        {
            return (a - b).sqrMagnitude < (ra + rb) * (ra + rb);
        }

        private bool IsOnLaserLine(Vector2 point, float radius)
        {
            Vector2 origin = _laserView.Origin;
            Vector2 dir = _laserView.Direction;
            Vector2 toPoint = point - origin;
            float dot = Vector2.Dot(toPoint, dir);
            if (dot < 0) return false;
            Vector2 closest = origin + dir * dot;
            return (closest - point).magnitude < radius;
        }

        private void ApplyKnockback(Vector2 enemyPosition)
        {
            Vector2 dir = (enemyPosition - _player.Body.Position).normalized;
            _player.Body.SetVelocity(-dir * 3f);
        }
    }
}