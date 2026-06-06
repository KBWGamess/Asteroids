using Asteroids.Core;
using UnityEngine;

namespace Asteroids.Infrastructure
{
    public interface IEnemyFactory
    {
        void CreateAsteroid(Vector2 position, Vector2 direction, float speed, EnemySize size = EnemySize.Large);
        void CreateUFO(Vector2 position, float speed);
    }
}