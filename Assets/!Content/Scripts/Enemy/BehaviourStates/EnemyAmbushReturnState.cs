#region Libraries

using Game.Scripts.Enum;
using UnityEngine;

#endregion

namespace Game.Scripts.Enemy.States
{
    public class EnemyAmbushReturnState : EnemyStateBase
    {
        private Vector3 _targetAmbushWorldPosition;

        public EnemyAmbushReturnState(EnemyViewModel enemyViewModel) : base(enemyViewModel) { }

        public override void Enter()
        {
            _enemyViewModel.SetState(EEnemyState.AmbushReturn);
            _enemyViewModel.SetDesiredSpeed(_enemyViewModel.EnemyConfig.RoamSpeed);
            
            if (AmbushPointsRegistry.Instance != null && AmbushPointsRegistry.Instance.TryGetNearestAmbush(_enemyViewModel.SelfPosition,
                    out Vector3 ambushWorldPosition))
                _targetAmbushWorldPosition = ambushWorldPosition;
            
            else
                _targetAmbushWorldPosition = _enemyViewModel.SelfPosition;

            _enemyViewModel.SetDestination(_targetAmbushWorldPosition);
        }

        public override void Update()
        {
            if (Reached(_targetAmbushWorldPosition))
            {
                _enemyViewModel.EnterIn(typeof(EnemyAmbushHoldState));
            }
        }

        public override void Exit()
        {
            
        }
    }
}

