using Asteroids.Core;
using Asteroids.Infrastructure;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Asteroids.Enemies
{
    public class AsteroidSpawner
    {
        private readonly IEnemyFactory _factory;
        private readonly EnemyConfig _config;
        private readonly WorldConfig _worldConfig;
        private CancellationTokenSource _cts;
        private UniTask _spawnTask;

        public AsteroidSpawner(
            IEnemyFactory factory,
            EnemyConfig config,
            WorldConfig worldConfig)
        {
            _factory = factory;
            _config = config;
            _worldConfig = worldConfig;
        }

        public void StartSpawning()
        {
            _cts = new CancellationTokenSource();
            _spawnTask = SpawnLoop(_cts.Token);
        }

        public void StopSpawning()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTask SpawnLoop(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await UniTask.WaitForSeconds(_config.spawnInterval, cancellationToken: token);
                    SpawnAsteroid();
                }
            }
            catch (System.OperationCanceledException)
            {
                // нормальная отмена
            }
            catch (System.Exception e)
            {
                Debug.LogError($"AsteroidSpawner error: {e}");
            }
        }

        private void SpawnAsteroid()
        {
            float halfWidth = _worldConfig.worldWidth / 2f;
            float halfHeight = _worldConfig.worldHeight / 2f;

            Vector2 position = GetSpawnPosition(halfWidth, halfHeight);
            Vector2 direction = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized;

            _factory.CreateAsteroid(position, direction, _config.asteroidSpeed);
        }

        private Vector2 GetSpawnPosition(float halfWidth, float halfHeight)
        {
            int side = Random.Range(0, 4);
            return side switch
            {
                0 => new Vector2(Random.Range(-halfWidth, halfWidth), halfHeight),
                1 => new Vector2(Random.Range(-halfWidth, halfWidth), -halfHeight),
                2 => new Vector2(halfWidth, Random.Range(-halfHeight, halfHeight)),
                _ => new Vector2(-halfWidth, Random.Range(-halfHeight, halfHeight))
            };
        }
    }
}