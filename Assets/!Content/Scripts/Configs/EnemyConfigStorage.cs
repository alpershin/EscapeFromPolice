#region Libraries

using Game.Scripts.Enum;
using Game.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Game.Scripts.Config
{
    [CreateAssetMenu(fileName = "EnemyConfigStorage", menuName = "Custom/Enemy/EnemyConfigStorage")]
    public class EnemyConfigStorage : CommonStorage<EEnemyArchetype, EnemyConfig>
    {
        [Button]
        public void AddConfig(EEnemyArchetype type)
        {
            if (_data.ContainsKey(type)) return;
            _data.Add(type, new EnemyConfig() { Archetype = type });
        }
    }
}