#region Libraries

using System;
using Game.Scripts.Config;
using Game.Scripts.Enemy.States;
using Game.Scripts.Enum;
using Game.Scripts.Interfaces;
using Game.Utilities.StateMachine;
using R3;
using UnityEngine;

#endregion

namespace Game.Scripts.Enemy
{
    public class EnemyViewModel : StateMachineBase
    {
        private readonly EnemyModel _enemyModel;
        private readonly EnemyConfig _enemyConfig;
        private readonly IEnemyPerception _enemyPerception;
        private readonly EEnemyArchetype _enemyArchetype;
        private readonly Vector3 _homeWorldPosition;
        private IDisposable _loseTargetTimerDisposable;
        private readonly ReactiveProperty<Vector3> _destinationWorldPositionProperty = new ReactiveProperty<Vector3>(Vector3.zero);
        private readonly ReactiveProperty<float> _desiredSpeedMetersPerSecondProperty = new ReactiveProperty<float>(0f);
        private readonly ReactiveProperty<EEnemyState> _currentStateProperty = new ReactiveProperty<EEnemyState>(EEnemyState.AmbushHold);

        public EEnemyArchetype EnemyArchetype => _enemyArchetype;
        public EnemyModel EnemyModel => _enemyModel;
        public EnemyConfig EnemyConfig => _enemyConfig;
        public Vector3 HomePosition => _homeWorldPosition;
        public Vector3 SelfPosition => _enemyPerception.SelfWorldPosition.CurrentValue;
        public Vector3 PlayerPosition => _enemyPerception.PlayerWorldPosition.CurrentValue;
        public ReadOnlyReactiveProperty<Vector3> DestinationWorldPosition => _destinationWorldPositionProperty;
        public ReadOnlyReactiveProperty<float> DesiredSpeedMetersPerSecond => _desiredSpeedMetersPerSecondProperty;
        public ReadOnlyReactiveProperty<EEnemyState> CurrentStateProperty => _currentStateProperty;

        public EnemyViewModel(EnemyModel enemyModel, EnemyConfig enemyConfig, IEnemyPerception enemyPerception,
            EEnemyArchetype enemyArchetype, Vector3 homeWorldPosition)
        {
            _enemyModel = enemyModel;
            _enemyConfig = enemyConfig;
            _enemyPerception = enemyPerception;
            _enemyArchetype = enemyArchetype;
            _homeWorldPosition = homeWorldPosition;

            InitializeStates();

            switch (_enemyArchetype)
            {
                case EEnemyArchetype.Chaser:
                    EnterIn<EnemyChaseState>();
                    break;
                case EEnemyArchetype.Ambusher:
                    EnterIn<EnemyAmbushHoldState>();
                    break;
                case EEnemyArchetype.Patroller:
                    EnterIn<EnemyRoamState>();
                    break;
            }

            _enemyPerception.PlayerIsVisible
                .Where(visible => visible)
                .Subscribe(_ => EnterIn<EnemyChaseState>())
                .AddTo(_lifetimeDisposable);

            _enemyPerception.PlayerIsVisible
                .Where(visible => !visible)
                .Subscribe(_ =>
                {
                    _loseTargetTimerDisposable?.Dispose();
                    _loseTargetTimerDisposable = Observable.Timer(TimeSpan.FromSeconds(_enemyConfig.LoseTargetSeconds))
                        .Subscribe(__ => OnLostPlayerTimeout())
                        .AddTo(_lifetimeDisposable);
                })
                .AddTo(_lifetimeDisposable);
        }

        public void SetDestination(Vector3 worldPosition) => _destinationWorldPositionProperty.Value = worldPosition;

        public void SetDesiredSpeed(float metersPerSecond) =>
            _desiredSpeedMetersPerSecondProperty.Value = metersPerSecond;

        public void SetState(EEnemyState state)
        {
            _enemyModel.CurrentState = state;
            _currentStateProperty.Value = state;
        }
        
        protected sealed override void InitializeStates()
        {
            AddState(new EnemyChaseState(this));
            AddState(new EnemyAmbushHoldState(this));
            AddState(new EnemyAmbushReturnState(this));
            AddState(new EnemyRoamState(this));
            AddState(new EnemySearchHangState(this));
        }

        private void OnLostPlayerTimeout()
        {
            if (_enemyPerception.PlayerIsVisible.CurrentValue) return;

            switch (_enemyArchetype)
            {
                case EEnemyArchetype.Chaser:
                    EnterIn<EnemySearchHangState>();
                    break;
                case EEnemyArchetype.Ambusher:
                    EnterIn<EnemyAmbushReturnState>();
                    break;
                case EEnemyArchetype.Patroller:
                    EnterIn<EnemyRoamState>();
                    break;
            }
        }
    }
}