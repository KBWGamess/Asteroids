using Asteroids.Enemies;
using Asteroids.Infrastructure;
using Asteroids.Player;
using UnityEngine;
using Zenject;

namespace Asteroids.Gameplay
{
    public class EnemyMovementSystem : ITickable
    {
        private readonly ObjectPool<AsteroidView> _asteroidPool;
        private readonly ObjectPool<UFOView> _ufoPool;
        private readonly PlayerModel _player;

        public EnemyMovementSystem(
            ObjectPool<AsteroidView> asteroidPool,
            ObjectPool<UFOView> ufoPool,
            PlayerModel player)
        {
            _asteroidPool = asteroidPool;
            _ufoPool = ufoPool;
            _player = player;
        }

        public void Tick()
        {
            foreach (var asteroid in _asteroidPool.GetActive())
            {
                if (asteroid.Model == null || !asteroid.Model.IsAlive) continue;
                asteroid.Model.Tick(Time.deltaTime);
            }

            foreach (var ufo in _ufoPool.GetActive())
            {
                if (ufo.Model == null || !ufo.Model.IsAlive) continue;
                ufo.Model.Tick(Time.deltaTime, _player.Body.Position);
                ufo.transform.position = ufo.Model.Body.Position;
            }
        }
    }
}