#region Libraries

using System.Collections.Generic;
using Game.Core;
using UnityEngine;

#endregion

public class AmbushPointsRegistry : MonoSingleton<AmbushPointsRegistry>
{
    [SerializeField] private List<Transform> _ambushPoints = new List<Transform>();

    public bool TryGetNearestAmbush(Vector3 fromWorldPosition, out Vector3 ambushWorldPosition)
    {
        ambushWorldPosition = default;
        if (_ambushPoints == null || _ambushPoints.Count == 0) return false;

        float bestSq = float.MaxValue;
        foreach (var t in _ambushPoints)
        {
            Vector3 p = t != null ? t.position : Vector3.zero;
            float sq = (p - fromWorldPosition).sqrMagnitude;
            if (sq < bestSq)
            {
                bestSq = sq;
                ambushWorldPosition = p;
            }
        }

        return bestSq < float.MaxValue;
    }
}