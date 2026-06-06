using Asteroids.Core;
using Asteroids.Infrastructure;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Asteroids.Enemies
{
    public class UFOSpawner
    {
        private readonly IEnemyFactory _factory;
        private readonly EnemyConfig _config;
        private readonly WorldConfig _worldConfig;
        private CancellationTokenSource _cts;

        public UFOSpawner(
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
            SpawnLoop(_cts.Token)
                .Forget(e =>
                {
                    if (e is System.OperationCanceledException) return;
                    Debug.LogError($"UFOSpawner error: {e}");
                });
        }

        public void StopSpawning()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTask SpawnLoop(CancellationToken token)
        {
            await UniTask.WaitForSeconds(_config.ufoSpawnDelay, cancellationToken: token);
            while (!token.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(
                    _config.spawnInterval * _config.ufoSpawnIntervalMultiplier,
                    cancellationToken: token);
                SpawnUFO();
            }
        }

        private void SpawnUFO()
        {
            float halfWidth = _worldConfig.worldWidth / 2f;
            float halfHeight = _worldConfig.worldHeight / 2f;

            int side = Random.Range(0, 4);
            Vector2 position = side switch
            {
                0 => new Vector2(Random.Range(-halfWidth, halfWidth), halfHeight),
                1 => new Vector2(Random.Range(-halfWidth, halfWidth), -halfHeight),
                2 => new Vector2(halfWidth, Random.Range(-halfHeight, halfHeight)),
                _ => new Vector2(-halfWidth, Random.Range(-halfHeight, halfHeight))
            };

            _factory.CreateUFO(position, _config.ufoSpeed);
        }
    }
}