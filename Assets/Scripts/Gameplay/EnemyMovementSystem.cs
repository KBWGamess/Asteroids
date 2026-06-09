using Asteroids.Core;
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
        private readonly WorldBounds _bounds;
        private readonly GameStateManager _stateManager;

        public EnemyMovementSystem(
            ObjectPool<AsteroidView> asteroidPool,
            ObjectPool<UFOView> ufoPool,
            PlayerModel player,
            WorldBounds bounds,
            GameStateManager stateManager)
        {
            _asteroidPool = asteroidPool;
            _ufoPool = ufoPool;
            _player = player;
            _bounds = bounds;
            _stateManager = stateManager;
        }

        public void Tick()
        {
            if (!_stateManager.IsPlaying) return;

            foreach (var asteroid in _asteroidPool.GetActive())
            {
                if (asteroid.Model == null || !asteroid.Model.IsAlive) continue;
                asteroid.Model.Tick(Time.deltaTime);
                asteroid.Model.Body.Position = _bounds.Wrap(asteroid.Model.Body.Position);
                asteroid.transform.position = asteroid.Model.Body.Position;
            }

            foreach (var ufo in _ufoPool.GetActive())
            {
                if (ufo.Model == null || !ufo.Model.IsAlive) continue;
                ufo.Model.Tick(Time.deltaTime, _player.Body.Position);
                ufo.Model.Body.Position = _bounds.Wrap(ufo.Model.Body.Position);
                ufo.transform.position = ufo.Model.Body.Position;
            }
        }
    }
}