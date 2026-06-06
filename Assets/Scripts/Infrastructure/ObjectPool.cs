using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids.Infrastructure
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly List<T> _pool = new List<T>();
        private readonly List<T> _activeCache = new List<T>();
        private readonly DiContainer _container;
        private readonly Transform _poolContainer;

        public ObjectPool(T prefab, DiContainer container)
        {
            _prefab = prefab;
            _container = container;
            
            GameObject containerGO = new GameObject($"Pool_{typeof(T).Name}");
            _poolContainer = containerGO.transform;
        }

        public T GetInactive()
        {
            foreach (var item in _pool)
                if (!item.gameObject.activeSelf)
                    return item;

            T newItem = _container.InstantiatePrefabForComponent<T>(_prefab);
            newItem.transform.SetParent(_poolContainer);
            newItem.gameObject.SetActive(false);
            _pool.Add(newItem);
            return newItem;
        }

        public void ReturnAll()
        {
            foreach (var item in _pool)
            {
                if (item.gameObject.activeSelf && item is IResettable poolable)
                    poolable.Deactivate();
            }
        }

        public IReadOnlyList<T> GetActive()
        {
            _activeCache.Clear();
            foreach (var item in _pool)
                if (item.gameObject.activeSelf)
                    _activeCache.Add(item);
            return _activeCache;
        }
    }
}