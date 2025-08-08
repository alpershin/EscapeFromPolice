#region Libraries

using UnityEngine;
using R3;

#endregion

namespace Game.Scripts.Core.Interfaces
{
    public interface IInputService
    {
        ReadOnlyReactiveProperty<Vector2> MoveAxis { get; }
    }
}