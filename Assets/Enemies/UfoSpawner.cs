using Asteroids.Core;
using Asteroids.Infrastructure;
using Asteroids.Player;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Asteroids.Enemies
{
    public class UfoSpawner
    {
        private readonly IEnemyFactory _factory;
        private readonly ObjectPool<UfoView> _pool;
        private readonly EnemyConfig _config;
        private readonly WorldConfig _worldConfig;
        private CancellationTokenSource _cts;

        public UfoSpawner(
            IEnemyFactory factory,
            ObjectPool<UfoView> pool,
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
            SpawnLoop(_cts.Token).Forget();
        }

        public void StopSpawning()
        {
            _cts?.Cancel();
        }

        private async UniTaskVoid SpawnLoop(CancellationToken token)
        {
            await UniTask.WaitForSeconds(5f, cancellationToken: token);
            while (!token.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(_config.spawnInterval * 2f, cancellationToken: token);
                SpawnUfo();
            }
        }

        private void SpawnUfo()
        {
            float hw = _worldConfig.worldWidth / 2f;
            float hh = _worldConfig.worldHeight / 2f;

            int side = Random.Range(0, 4);
            Vector2 position = side switch
            {
                0 => new Vector2(Random.Range(-hw, hw), hh),
                1 => new Vector2(Random.Range(-hw, hw), -hh),
                2 => new Vector2(hw, Random.Range(-hh, hh)),
                _ => new Vector2(-hw, Random.Range(-hh, hh))
            };

            _factory.CreateUfo(position, _config.ufoSpeed);
        }

        public void Tick(Vector2 playerPosition)
        {
            foreach (var ufo in _pool.GetActive())
            {
                if (ufo.Model == null || !ufo.Model.IsAlive) continue;
                ufo.Model.Tick(Time.deltaTime, playerPosition);
            }
        }
    }
}