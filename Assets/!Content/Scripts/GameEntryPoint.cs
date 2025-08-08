#region Libraries

using Game.Scripts.Core;
using Game.Scripts.Player;
using UnityEngine;


#endregion

namespace Game.Scripts
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerEntryPoint _player;
        [SerializeField] private PlayerInput _input;

        private void Awake()
        {
            _player.Construct(_input);
        }
    }
}