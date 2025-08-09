#region Libraries

using Game.Scripts.Enemy;
using Game.Scripts.Enemy.States;
using Game.Scripts.Enum;
using UnityEngine;

#endregion

namespace Game.Scripts.Enemy.States
{
    public class EnemyAmbushHoldState : EnemyStateBase
    {
        private Vector3 _holdWorldPosition;

        public EnemyAmbushHoldState(EnemyViewModel enemyViewModel) : base(enemyViewModel) { }

        public override void Enter()
        {
            _enemyViewModel.SetState(EEnemyState.AmbushHold);
            _enemyViewModel.SetDesiredSpeed(0f);

            _holdWorldPosition = _enemyViewModel.SelfPosition;
            _enemyViewModel.SetDestination(_holdWorldPosition);
        }

        public override void Update()
        {

        }

        public override void Exit()
        {

        }
    }
}