#region Libraries

using EasyMobileInput;
using Game.Scripts.Core.Interfaces;
using R3;
using UnityEngine;

#endregion

namespace Game.Scripts.Core
{
    public class PlayerInput : MonoBehaviour, IInputService
    {
        [SerializeField] private Joystick _joystick;
        
        private ReactiveProperty<Vector2> _move = new(Vector2.zero);
        
        public ReadOnlyReactiveProperty<Vector2> MoveAxis => _move;

        private void Update()
        {
            _move.Value= new Vector2(_joystick.CurrentProcessedValue.x, _joystick.CurrentProcessedValue.y);
        }
    }
}