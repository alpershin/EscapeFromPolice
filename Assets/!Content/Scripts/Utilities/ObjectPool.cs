namespace Game.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected GameObject _container;
        [SerializeField] protected int _capacity;

        private List<T> _pool = new List<T>();

        protected void Initialize(T prefab)
        {
            for (int i = 0; i < _capacity; i++)
            {
                T spawned = Instantiate(prefab, _container.transform);
                spawned.gameObject.SetActive(false);

                _pool.Add(spawned);
            }
        }

        protected virtual int GetInactiveObjectCount()
        {
            int result;
            return result = _pool.Count(p => p.gameObject.activeSelf == false);
        }
    
        protected bool TryGetObject(out T result)
        {
            result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

            return result != null;
        }

        protected void ClearPool()
        {
            foreach (var go in _pool)
                Destroy(go.gameObject);
            
            _pool.Clear();
        }
    } 
}