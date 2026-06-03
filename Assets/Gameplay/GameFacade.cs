using Asteroids.Enemies;

namespace Asteroids.Gameplay
{
    public class GameFacade
    {
        private readonly AsteroidSpawner _asteroidSpawner;
        private readonly UFOSpawner _ufoSpawner;
        private readonly ScoreService _scoreService;

        public GameFacade(
            AsteroidSpawner asteroidSpawner,
            UFOSpawner ufoSpawner,
            ScoreService scoreService)
        {
            _asteroidSpawner = asteroidSpawner;
            _ufoSpawner = ufoSpawner;
            _scoreService = scoreService;
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

        public int GetScore() => _scoreService.Score;
    }
}