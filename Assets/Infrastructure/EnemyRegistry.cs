using System.Collections.Generic;
using Asteroids.Core;

namespace Asteroids.Infrastructure
{
    public class EnemyRegistry
    {
        private readonly List<IEnemy> _enemies = new List<IEnemy>();

        public void Register(IEnemy enemy)
        {
            if (!_enemies.Contains(enemy))
                _enemies.Add(enemy);
        }

        public void Unregister(IEnemy enemy)
        {
            _enemies.Remove(enemy);
        }

        public IReadOnlyList<IEnemy> GetAll() => _enemies;
        public int Count => _enemies.Count;
    }
}