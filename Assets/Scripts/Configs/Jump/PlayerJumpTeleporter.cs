using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Characters Configs", menuName = "CharactersConfigs / Teleporter Jump Parameters")]
    public class PlayerJumpTeleporter : ScriptableObject
    {
        [field: SerializeField] public float JumpTime { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public float HangTime { get; private set; }
    }
}