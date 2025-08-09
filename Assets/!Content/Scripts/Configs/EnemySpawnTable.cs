#region Libraries

using System;
using Game.Scripts.Config;
using Game.Scripts.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnemySpawnTable", menuName = "Custom/Enemy/EnemySpawnTable")]
    public class EnemySpawnTable : SerializedScriptableObject
    {
        public Entry[] Entries;
    }

    [Serializable]
    public class Entry
    {
        public EEnemyArchetype Archetype;
        [Min(0f)] public float Weight = 1f;
        public GameObject EnemyPrefab;
    }
}