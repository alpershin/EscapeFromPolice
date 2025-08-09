#region Libraries

using Game.Scripts.Enum;
using UnityEngine;

#endregion

namespace Game.Scripts.Enemy.States
{
    public class EnemyRoamState : EnemyStateBase
    {
        private Vector3 _currentRoamTargetWorldPosition;
        private float _nextRepickTimeSeconds;

        public EnemyRoamState(EnemyViewModel enemyViewModel) : base(enemyViewModel)
        {
        }

        public override void Enter()
        {
            _enemyViewModel.SetState(EEnemyState.Roam);
            _enemyViewModel.SetDesiredSpeed(_enemyViewModel.EnemyConfig.RoamSpeed);
            PickNewRoamTarget(forceImmediatePick: true);
        }

        public override void Update()
        {
            bool reached =
                (_enemyViewModel.SelfPosition - _currentRoamTargetWorldPosition).sqrMagnitude
                < _enemyViewModel.EnemyConfig.RoamMinHopMeters * _enemyViewModel.EnemyConfig.RoamMinHopMeters;

            if (Time.time >= _nextRepickTimeSeconds || reached)
            {
                PickNewRoamTarget(forceImmediatePick: false);
            }

            _enemyViewModel.SetDestination(_currentRoamTargetWorldPosition);
        }

        public override void Exit()
        {
        }

        private void PickNewRoamTarget(bool forceImmediatePick)
        {
            _nextRepickTimeSeconds = Time.time + _enemyViewModel.EnemyConfig.RoamPickIntervalSeconds;

            if (TryPickForwardBiasedPoint(
                    selfWorldPosition: _enemyViewModel.SelfPosition,
                    homeWorldPosition: _enemyViewModel.HomePosition,
                    resultWorldPosition: out Vector3 candidate))
            {
                _currentRoamTargetWorldPosition = candidate;
            }
            else
            {
                _currentRoamTargetWorldPosition = _enemyViewModel.SelfPosition;
            }
        }

        private bool TryPickForwardBiasedPoint(
            Vector3 selfWorldPosition,
            Vector3 homeWorldPosition,
            out Vector3 resultWorldPosition)
        {
            resultWorldPosition = selfWorldPosition;

            Vector3 vectorToHome = (homeWorldPosition - selfWorldPosition);
            Vector3 outwardDirection = (-vectorToHome).normalized; // «от дома»
            if (outwardDirection == Vector3.zero) outwardDirection = Vector3.forward;

            float halfSectorDegrees = _enemyViewModel.EnemyConfig.RoamForwardBiasDegrees * 0.5f;

            for (int attemptIndex = 0; attemptIndex < _enemyViewModel.EnemyConfig.RoamSampleTries; attemptIndex++)
            {
                float randomAngleDegrees = Random.Range(-halfSectorDegrees, halfSectorDegrees);
                Quaternion rotationAroundUp = Quaternion.AngleAxis(randomAngleDegrees, Vector3.up);
                Vector3 biasedDirection = rotationAroundUp * outwardDirection;

                float randomDistanceMeters = Random.Range(
                    Mathf.Max(1f, _enemyViewModel.EnemyConfig.RoamMinHopMeters),
                    _enemyViewModel.EnemyConfig.RoamRadiusMeters);

                Vector3 candidateWorldPosition = selfWorldPosition + biasedDirection.normalized * randomDistanceMeters;

                if ((candidateWorldPosition - homeWorldPosition).sqrMagnitude
                    <= _enemyViewModel.EnemyConfig.RoamRadiusMeters * _enemyViewModel.EnemyConfig.RoamRadiusMeters)
                {
                    resultWorldPosition = candidateWorldPosition;
                    return true;
                }
            }

            return false;
        }
    }
}