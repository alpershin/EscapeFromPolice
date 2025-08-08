#region Libraries

using R3;
using UnityEngine;

#endregion

public class PlayerMovementView : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private CompositeDisposable _life = new();
    private Vector3 _targetVelocity;
    private float _acceleration;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Bind(PlayerMovementViewModel viewModel)
    {
        viewModel.TargetVelocity
            .Subscribe(target =>
            {
                _acceleration = viewModel.Acceleration;
                _targetVelocity = target;
            })
            .AddTo(_life);
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = Vector3.MoveTowards(_rigidbody.linearVelocity, _targetVelocity, _acceleration * Time.fixedDeltaTime);
    }

    private void OnDestroy() => _life.Dispose();
}
