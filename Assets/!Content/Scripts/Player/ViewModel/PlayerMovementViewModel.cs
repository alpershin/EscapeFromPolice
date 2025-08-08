#region Libraries

using Game.Scripts.Configs;
using Game.Scripts.Player;
using R3;
using UnityEngine;

#endregion

public class PlayerMovementViewModel
{
    private readonly PlayerModel _model;
    private readonly ReadOnlyReactiveProperty<Vector2> _moveAxis;
    private readonly PlayerConfig _config;

    public ReadOnlyReactiveProperty<Vector3> TargetVelocity { get; }
    public float Acceleration => _config.Acceleration;

    public PlayerMovementViewModel(PlayerModel model, ReadOnlyReactiveProperty<Vector2> moveAxis, PlayerConfig config)
    {
        _model = model;
        _moveAxis = moveAxis;
        _config = config;

        TargetVelocity = _moveAxis
            .Select(axis =>
            {
                var input = Vector2.ClampMagnitude(axis, 1f);
                var targetVel = new Vector3(input.x, 0f, input.y) * config.MaxSpeed;
                return targetVel;
            })
            .ToReadOnlyReactiveProperty();
    }
}
