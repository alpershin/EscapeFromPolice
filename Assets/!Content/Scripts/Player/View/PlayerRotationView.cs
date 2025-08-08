#region Libraries

using R3;
using UnityEngine;

#endregion

public class PlayerRotationView : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private CompositeDisposable _life = new();
    private Vector3 _targetLook;

    public ReactiveProperty<Vector3> VelocityStream = new ReactiveProperty<Vector3>();
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void Bind(PlayerRotationViewModel viewModel)
    {
        viewModel.LookDirection
            .Subscribe(dir => _targetLook = dir)
            .AddTo(_life);
        
        Observable.EveryUpdate(UnityFrameProvider.FixedUpdate)
            .Where(_ => _targetLook != Vector3.zero)
            .Subscribe(_ =>
            {
                var targetRot = Quaternion.LookRotation(_targetLook, Vector3.up);
                var newRot = Quaternion.Lerp(_rigidBody.rotation, targetRot, viewModel.RotationAngle * Time.fixedDeltaTime);

                _rigidBody.MoveRotation(newRot);
            })
            .AddTo(_life);
    }

    private void FixedUpdate()
    {
        VelocityStream.Value = _rigidBody.linearVelocity;
    }

    private void OnDestroy() => _life.Dispose();
}
