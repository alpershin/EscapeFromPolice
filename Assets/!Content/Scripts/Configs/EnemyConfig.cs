#region Libraries

using System;
using Game.Scripts.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Game.Scripts.Config
{
    [Serializable]
    public class EnemyConfig
    {
        [HideInInspector] public EEnemyArchetype Archetype;

        [Header("Base Speeds (m/s)")] public float ChaseSpeed = 5.5f;
        public float RoamSpeed = 3.5f;

        [Header("Perception")] 
        public float SenseRadius = 12f;
        public float LoseTargetSeconds = 0.8f;
        public LayerMask LineOfSightMask;
        
        [Header("Roam Settings")]
        [ShowIf("Archetype", EEnemyArchetype.Patroller)]
        public float RoamRadiusMeters = 15f;

        [ShowIf("Archetype", EEnemyArchetype.Patroller)]
        public float RoamPickIntervalSeconds = 8f;

        [ShowIf("Archetype", EEnemyArchetype.Patroller)]
        public float RoamMinHopMeters = 3f;

        [ShowIf("Archetype", EEnemyArchetype.Patroller)]
        public float RoamForwardBiasDegrees = 100f;

        [ShowIf("Archetype", EEnemyArchetype.Patroller)]
        public int RoamSampleTries = 8;

        [Header("Search (for Chaser)")] 
        [ShowIf("Archetype", EEnemyArchetype.Chaser)]
        public float SearchHangSeconds = 1.5f;
    }
}