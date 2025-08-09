#region Libraries

using Game.Scripts.Config;
using Game.Scripts.Configs;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Game.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemySpawnTable _enemySpawnTable;
        [SerializeField] private EnemyConfigStorage _configStorage;
        [SerializeField] private Transform _playerTransform;
        
        [Button]
        public GameObject SpawnOnce()
        {
            var selected = SelectByWeight(_enemySpawnTable.Entries);
            if (selected == null || selected.EnemyPrefab == null)
                return null;

            var enemy = Instantiate(selected.EnemyPrefab, transform.position, transform.rotation);

            var enemyEntryPoint = enemy.GetComponent<EnemyEntryPoint>();
            if (enemyEntryPoint != null)
                enemyEntryPoint.Construct(_configStorage.GetValue(selected.Archetype), selected.Archetype, transform.position, _playerTransform);
            
            return enemy;
        }

        private Entry SelectByWeight(Entry[] entries)
        {
            if (entries == null || entries.Length == 0) return null;

            float sum = 0f;
            for (int i = 0; i < entries.Length; i++) sum += Mathf.Max(0f, entries[i].Weight);
            if (sum <= 0f) return entries[0];

            float r = Random.value * sum;
            float acc = 0f;
            for (int i = 0; i < entries.Length; i++)
            {
                acc += Mathf.Max(0f, entries[i].Weight);
                if (r <= acc) return entries[i];
            }
            return entries[^1];
        }
    }
}