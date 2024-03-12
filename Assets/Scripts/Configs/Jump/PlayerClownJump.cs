using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Characters Configs", menuName = "CharactersConfigs / Clown Jump Parameters")]
    public class PlayerClownJump : ScriptableObject
    {
        [field: SerializeField] public float JumpTime { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
    }
}