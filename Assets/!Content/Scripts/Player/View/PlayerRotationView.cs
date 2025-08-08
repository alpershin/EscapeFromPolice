#region Libraries

using System;
using R3;
using UnityEngine;

#endregion

public class PlayerRotationView : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private CompositeDisposable _life = new();

    public ReactiveProperty<Vector3> VelocityStream = new ReactiveProperty<Vector3>();
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void Bind(PlayerRotationViewModel viewModel)
    {
        viewModel.LookDirection
            .Subscribe(lookDir =>
            {
                if (lookDir == Vector3.zero) return;
                var targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
                _rigidBody.rotation = Quaternion.Slerp(
                    _rigidBody.rotation,
                    targetRot,
                    viewModel.RotationAngle * Time.deltaTime);
            })
            .AddTo(_life);
    }

    private void FixedUpdate()
    {
        VelocityStream.Value = _rigidBody.linearVelocity;
    }

    private void OnDestroy() => _life.Dispose();
}
