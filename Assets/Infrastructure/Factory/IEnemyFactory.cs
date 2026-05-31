using UnityEngine;

namespace Asteroids.Infrastructure
{
    public interface IEnemyFactory
    {
        void CreateAsteroid(Vector2 position, Vector2 direction, float speed);
        void CreateUfo(Vector2 position, float speed);
    }
}