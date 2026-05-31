using Asteroids.Enemies;
using Asteroids.Player;
using Zenject;
using Asteroids.Weapons;
using Asteroids.Core;
using UnityEngine;

namespace Asteroids.Gameplay
{
    public class GameFacade
    {
        private readonly PlayerModel _player;
        private readonly AsteroidSpawner _asteroidSpawner;
        private readonly UfoSpawner _ufoSpawner;
        private readonly ScoreSystem _scoreSystem;
        private readonly SignalBus _signalBus;

        public GameFacade(
            PlayerModel player,
            AsteroidSpawner asteroidSpawner,
            UfoSpawner ufoSpawner,
            ScoreSystem scoreSystem,
            SignalBus signalBus)
        {
            _player = player;
            _asteroidSpawner = asteroidSpawner;
            _ufoSpawner = ufoSpawner;
            _scoreSystem = scoreSystem;
            _signalBus = signalBus;
        }

        public void StartGame()
        {
            _asteroidSpawner.StartSpawning();
            _ufoSpawner.StartSpawning();
        }

        public void StopGame()
        {
            _asteroidSpawner.StopSpawning();
            _ufoSpawner.StopSpawning();
        }

        public int GetScore() => _scoreSystem.Score;
        public int GetHealth() => _player.Health;
        public bool IsPlayerDead() => _player.IsDead();
    }
}