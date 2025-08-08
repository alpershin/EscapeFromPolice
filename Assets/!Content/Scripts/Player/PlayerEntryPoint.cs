#region Libraries

using Game.Scripts.Configs;
using Game.Scripts.Core.Interfaces;
using UnityEngine;

#endregion

namespace Game.Scripts.Player
{
    public class PlayerEntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerMovementView _movementView;
        [SerializeField] private PlayerRotationView _rotationView;
        [SerializeField] private PlayerConfig _playerConfig;

        private PlayerModel _model;
        private PlayerMovementViewModel _movementViewModel;
        private PlayerRotationViewModel _rotationViewModel;

        public void Construct(IInputService input)
        {
            _model = new PlayerModel();

            _movementViewModel = new PlayerMovementViewModel(
                _model,
                input.MoveAxis,
                _playerConfig);

            _rotationViewModel = new PlayerRotationViewModel(_rotationView.VelocityStream, _playerConfig);

            _movementView.Bind(_movementViewModel);
            _rotationView.Bind(_rotationViewModel);
        }
    }
}

