#region Libraries

using Game.Utilities.StateMachine;
using UnityEngine;

#endregion

namespace Game.Scripts.Enemy.States
{
    public abstract class EnemyStateBase : IStateBase
    {
        protected readonly EnemyViewModel _enemyViewModel;

        protected EnemyStateBase(EnemyViewModel enemyViewModel)
        {
            _enemyViewModel = enemyViewModel;
        }

        public abstract void Enter();

        public abstract void Update();

        public abstract void Exit();
        
        protected bool Reached(Vector3 targetWorldPosition, float thresholdMeters = 0.4f)
        {
            Vector3 delta = _enemyViewModel.SelfPosition - targetWorldPosition;
            delta.y = 0f;
            return delta.sqrMagnitude <= thresholdMeters * thresholdMeters;
        }
    }
}