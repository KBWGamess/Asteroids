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
            SpawnLoop(_cts.Token).Forget();
        }

        public void StopSpawning()
        {
            _cts?.Cancel();
        }

        private async UniTaskVoid SpawnLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(_config.spawnInterval, cancellationToken: token);
                SpawnAsteroid();
            }
        }

        private void SpawnAsteroid()
        {
            float hw = _worldConfig.worldWidth / 2f;
            float hh = _worldConfig.worldHeight / 2f;

            Vector2 position = GetSpawnPosition(hw, hh);
            Vector2 direction = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized;

            _factory.CreateAsteroid(position, direction, _config.asteroidSpeed);
        }

        private Vector2 GetSpawnPosition(float hw, float hh)
        {
            int side = Random.Range(0, 4);
            return side switch
            {
                0 => new Vector2(Random.Range(-hw, hw), hh),
                1 => new Vector2(Random.Range(-hw, hw), -hh),
                2 => new Vector2(hw, Random.Range(-hh, hh)),
                _ => new Vector2(-hw, Random.Range(-hh, hh))
            };
        }
    }
}