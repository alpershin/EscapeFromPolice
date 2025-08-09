#region Libraries

using Game.Scripts.Config;
using Game.Scripts.Enum;
using Game.Scripts.Interfaces;
using R3;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Game.Scripts.Enemy
{
    public class EnemyEntryPoint : MonoBehaviour
    {
        private EnemyConfig _enemyConfig;
        private EEnemyArchetype _enemyArchetype;
        private Transform _playerTransform;
        private Vector3 _homeAnchor;

        private NavMeshAgent _navMeshAgent;
        private EnemyNavMeshView _enemyNavMeshView;

        private EnemyModel _enemyModel;
        private EnemyViewModel _enemyViewModel;

        private CompositeDisposable _lifetimeDisposable = new CompositeDisposable();

        private void Awake()
        {
            _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            _enemyNavMeshView = GetComponent<EnemyNavMeshView>();
        }

        public void Construct(EnemyConfig enemyConfig, EEnemyArchetype enemyArchetype, Vector3 home, Transform player)
        {
            _enemyConfig = enemyConfig;
            _enemyArchetype = enemyArchetype;
            _homeAnchor = home;
            _playerTransform = player;
            
            if (_enemyConfig == null)
            {
                Debug.LogError("[EnemyEntryPoint] EnemyConfig is not assigned.");
                enabled = false;
                return;
            }
            
            _enemyNavMeshView.ConfigurePerception(_enemyConfig, _playerTransform);
            
            _enemyModel = new EnemyModel();
            Vector3 homeWorldPosition = _homeAnchor != null ? _homeAnchor : transform.position;
            
            _enemyViewModel = new EnemyViewModel(_enemyModel, _enemyConfig, _enemyNavMeshView, _enemyArchetype, homeWorldPosition);
            
            _enemyViewModel.DesiredSpeedMetersPerSecond
                .Subscribe(desiredSpeed => _navMeshAgent.speed = desiredSpeed)
                .AddTo(_lifetimeDisposable);
            
            _enemyViewModel.DestinationWorldPosition
                .Subscribe(destination =>
                {
                    if (NavMesh.SamplePosition(destination, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                        _navMeshAgent.SetDestination(hit.position);
                })
                .AddTo(_lifetimeDisposable);

        }

        private void OnDestroy()
        {
            _lifetimeDisposable.Dispose();
            _enemyViewModel?.Dispose();
        }
    }
}