using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Infrastructure
{
    public class EnemyRegistry
    {
        private readonly List<GameObject> _enemies = new List<GameObject>();

        public void Register(GameObject enemy)
        {
            if (!_enemies.Contains(enemy))
                _enemies.Add(enemy);
        }

        public void Unregister(GameObject enemy)
        {
            _enemies.Remove(enemy);
        }

        public List<GameObject> GetAll() => _enemies;
        public int Count => _enemies.Count;
    }
}