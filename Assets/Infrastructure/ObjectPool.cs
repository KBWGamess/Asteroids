using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Infrastructure
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly List<T> _pool = new List<T>();

        public ObjectPool(T prefab)
        {
            _prefab = prefab;
        }

        public T Get()
        {
            foreach (var item in _pool)
                if (!item.gameObject.activeSelf)
                    return item;

            T newItem = Object.Instantiate(_prefab);
            newItem.transform.position = new UnityEngine.Vector3(9999f, 9999f, 0f);
            newItem.gameObject.SetActive(false);
            _pool.Add(newItem);
            return newItem;
        }

        public void ReturnAll()
        {
            foreach (var item in _pool)
                item.gameObject.SetActive(false);
        }
        
        public List<T> GetActive()
        {
            var active = new List<T>();
            foreach (var item in _pool)
                if (item.gameObject.activeSelf)
                    active.Add(item);
            return active;
        }
    }
}