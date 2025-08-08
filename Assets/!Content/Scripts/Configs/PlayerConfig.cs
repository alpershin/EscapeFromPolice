#region Libraries

using Sirenix.OdinInspector;
using UnityEngine;
#endregion

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Custom/Player/PlayerConfig")]
    public class PlayerConfig : SerializedScriptableObject
    {
        public float MaxSpeed;
        public float Acceleration;
        public float RotationAngle;
    }
}

