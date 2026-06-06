using Asteroids.Enemies;
using UnityEngine;
using Zenject;

namespace Asteroids.Gameplay
{
    public class GameStartup : MonoBehaviour
    {
        private AsteroidSpawner _asteroidSpawner;
        private UFOSpawner _ufoSpawner;

        [Inject]
        public void Construct(AsteroidSpawner asteroidSpawner, UFOSpawner ufoSpawner)
        {
            _asteroidSpawner = asteroidSpawner;
            _ufoSpawner = ufoSpawner;
        }

        private void Start()
        {
            _asteroidSpawner.StartSpawning();
            _ufoSpawner.StartSpawning();
        }

        private void OnDestroy()
        {
            _asteroidSpawner.StopSpawning();
            _ufoSpawner.StopSpawning();
        }
    }
}