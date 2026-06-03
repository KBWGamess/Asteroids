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
        private readonly ObjectPool<UFOView> _pool;
        private readonly EnemyConfig _config;
        private readonly WorldConfig _worldConfig;
        private CancellationTokenSource _cts;
        private UniTask _spawnTask;

        public UFOSpawner(
            IEnemyFactory factory,
            ObjectPool<UFOView> pool,
            EnemyConfig config,
            WorldConfig worldConfig)
        {
            _factory = factory;
            _pool = pool;
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
                await UniTask.WaitForSeconds(5f, cancellationToken: token);
                while (!token.IsCancellationRequested)
                {
                    await UniTask.WaitForSeconds(_config.spawnInterval * 2f, cancellationToken: token);
                    SpawnUFO();
                }
            }
            catch (System.OperationCanceledException)
            {
                // нормальная отмена
            }
            catch (System.Exception e)
            {
                Debug.LogError($"UFOSpawner error: {e}");
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