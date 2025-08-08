#region Libraries

using System;
using Game.Scripts.Configs;
using R3;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

#endregion

public class PlayerRotationViewModel
{
    private PlayerConfig _playerConfig;
    
    public ReadOnlyReactiveProperty<Vector3> LookDirection { get; }
    public float RotationAngle => _playerConfig.RotationAngle;

    public PlayerRotationViewModel(ReadOnlyReactiveProperty<Vector3> velocityStream, PlayerConfig config)
    {
        _playerConfig = config;
        
        LookDirection = velocityStream
            .Select(vel =>
            {
                var flat = new Vector3(vel.x, 0f, vel.z);
                return flat.normalized;
            })
            .ToReadOnlyReactiveProperty();
    }
}
