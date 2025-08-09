#region Libraries

using Game.Scripts.Enum;
using UnityEngine;

#endregion

public class EnemyModel
{
    /// <summary>
    /// Текущее состояние врага в терминах логики (FSM).
    /// </summary>
    public EEnemyState CurrentState { get; set; } = EEnemyState.Idle;

    /// <summary>
    /// Последняя известная позиция игрока.
    /// </summary>
    public Vector3 LastKnownPlayerPosition { get; set; } = Vector3.zero;

    /// <summary>
    /// Сколько секунд прошло с момента, когда враг видел игрока.
    /// </summary>
    public float TimeSinceLastSawPlayer { get; set; } = 0f;

    /// <summary>
    /// Жив ли враг (если будет система здоровья).
    /// </summary>
    public bool IsAlive { get; set; } = true;

    /// <summary>
    /// Текущее здоровье врага (если будет нужно).
    /// </summary>
    public int Health { get; set; } = 100;

    public void ResetPerception()
    {
        LastKnownPlayerPosition = Vector3.zero;
        TimeSinceLastSawPlayer = 0f;
    }
}
