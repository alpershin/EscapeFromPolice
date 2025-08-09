#region Libraries

using R3;
using UnityEngine;

#endregion

namespace Game.Scripts.Interfaces
{
    public interface IEnemyPerception
    {
        ReadOnlyReactiveProperty<bool> PlayerIsVisible { get; }
        ReadOnlyReactiveProperty<Vector3> PlayerWorldPosition { get; }
        ReadOnlyReactiveProperty<Vector3> SelfWorldPosition { get; }
    }
}