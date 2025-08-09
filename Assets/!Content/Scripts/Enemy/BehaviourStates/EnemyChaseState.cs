#region Libraries

using Game.Scripts.Enemy;
using Game.Scripts.Enemy.States;
using Game.Scripts.Enum;
using UnityEngine;

#endregion

public class EnemyChaseState : EnemyStateBase
{
    public EnemyChaseState(EnemyViewModel enemyViewModel) : base(enemyViewModel) { }

    public override void Enter()
    {
        _enemyViewModel.SetState(EEnemyState.Chase);
        _enemyViewModel.SetDesiredSpeed(_enemyViewModel.EnemyConfig.ChaseSpeed);
    }

    public override void Update()
    {
        Vector3 playerWorldPosition = _enemyViewModel.PlayerPosition;
        _enemyViewModel.SetDestination(playerWorldPosition);
        _enemyViewModel.EnemyModel.LastKnownPlayerPosition = playerWorldPosition;
    }

    public override void Exit()
    {
        //throw new System.NotImplementedException();
    }
}
