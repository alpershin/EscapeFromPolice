#region Libraries

using Game.Scripts.Enemy;
using Game.Scripts.Enemy.States;
using Game.Scripts.Enum;
using UnityEngine;

#endregion

namespace Game.Scripts.Enemy.States
{
    public class EnemySearchHangState : EnemyStateBase
    {
        private Vector3 _targetLastKnownPlayerWorldPosition;
        private float _leaveTimeSeconds;

        public EnemySearchHangState(EnemyViewModel enemyViewModel) : base(enemyViewModel)
        {
        }

        public override void Enter()
        {
            _enemyViewModel.SetState(EEnemyState.SearchHang);
            _enemyViewModel.SetDesiredSpeed(0f);

            _targetLastKnownPlayerWorldPosition = _enemyViewModel.EnemyModel.LastKnownPlayerPosition;
            if (_targetLastKnownPlayerWorldPosition == Vector3.zero)
                _targetLastKnownPlayerWorldPosition = _enemyViewModel.PlayerPosition;

            _enemyViewModel.SetDestination(_targetLastKnownPlayerWorldPosition);
            _leaveTimeSeconds = Time.time + _enemyViewModel.EnemyConfig.SearchHangSeconds;
        }

        public override void Update()
        {
            if (Time.time >= _leaveTimeSeconds)
            {
                switch (_enemyViewModel.EnemyArchetype)
                {
                    case EEnemyArchetype.Chaser:
                        _enemyViewModel.EnterIn(typeof(EnemyChaseState));
                        break;

                    case EEnemyArchetype.Ambusher:
                        _enemyViewModel.EnterIn(typeof(EnemyAmbushReturnState));
                        break;

                    case EEnemyArchetype.Patroller:
                        _enemyViewModel.EnterIn(typeof(EnemyRoamState));
                        break;
                }
            }
        }

        public override void Exit()
        {
            
        }
    }
}