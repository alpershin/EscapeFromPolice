#region Libraries

using System;
using Game.Scripts.Config;
using Game.Scripts.Interfaces;
using R3;
using UnityEngine;

#endregion

namespace Game.Scripts.Enemy
{
    public class EnemyNavMeshView : MonoBehaviour, IEnemyPerception, IDisposable
    {
        private Transform _playerTransform;
        private EnemyConfig _enemyConfig;

        private CompositeDisposable _lifetimeDisposable = new CompositeDisposable();
        
        private readonly ReactiveProperty<bool> _playerIsVisibleProperty = new ReactiveProperty<bool>(false);
        private readonly ReactiveProperty<Vector3> _playerWorldPositionProperty = new ReactiveProperty<Vector3>(Vector3.zero);
        private readonly ReactiveProperty<Vector3> _selfWorldPositionProperty = new ReactiveProperty<Vector3>(Vector3.zero);

        public ReadOnlyReactiveProperty<bool> PlayerIsVisible => _playerIsVisibleProperty;
        public ReadOnlyReactiveProperty<Vector3> PlayerWorldPosition => _playerWorldPositionProperty;
        public ReadOnlyReactiveProperty<Vector3> SelfWorldPosition => _selfWorldPositionProperty;
        
        public void ConfigurePerception(EnemyConfig enemyConfig, Transform playerTransform)
        {
            _enemyConfig = enemyConfig;
            _playerTransform = playerTransform;

            _lifetimeDisposable?.Dispose();
            _lifetimeDisposable = new CompositeDisposable();

            Observable.EveryUpdate(UnityFrameProvider.Update)
                .Subscribe(_ =>
                {
                    _selfWorldPositionProperty.Value = transform.position;
                    UpdatePlayerPerception();
                })
                .AddTo(_lifetimeDisposable);
        }

        private void UpdatePlayerPerception()
        {
            if (_playerTransform == null || _enemyConfig == null)
            {
                _playerIsVisibleProperty.Value = false;
                return;
            }

            _playerWorldPositionProperty.Value = _playerTransform.position;

            Vector3 vectorToPlayer = _playerTransform.position - transform.position;
            bool withinSenseRadius =
                vectorToPlayer.sqrMagnitude <= _enemyConfig.SenseRadius * _enemyConfig.SenseRadius;

            bool hasLineOfSight = !Physics.Raycast(
                origin: transform.position + Vector3.up * 0.5f,
                direction: vectorToPlayer.normalized,
                maxDistance: vectorToPlayer.magnitude,
                layerMask: _enemyConfig.LineOfSightMask,
                queryTriggerInteraction: QueryTriggerInteraction.Ignore);

            _playerIsVisibleProperty.Value = withinSenseRadius && hasLineOfSight;
        }

        public void Dispose()
        {
            _lifetimeDisposable?.Dispose();
            _playerIsVisibleProperty?.Dispose();
            _playerWorldPositionProperty?.Dispose();
            _selfWorldPositionProperty?.Dispose();
            _lifetimeDisposable?.Dispose();
        }
        
        private void OnDestroy()
        {
            Dispose();
        }
    }
}