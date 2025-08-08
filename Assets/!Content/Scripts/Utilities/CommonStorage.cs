namespace Game.Utilities
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class CommonStorage<T, T1> : SerializedScriptableObject
    {
        [SerializeField] protected Dictionary<T, T1> _data;

        public Dictionary<T, T1> Data => _data;
        
        public virtual T1 GetValue(T key)
        {
            if (!_data.TryGetValue(key, out var value))
                throw new ArgumentOutOfRangeException($"Key {key} not found in storage.");

            return value;
        }
    }
}