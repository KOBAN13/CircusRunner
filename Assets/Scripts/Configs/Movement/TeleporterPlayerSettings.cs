using Character;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Characters Configs", menuName = "CharactersConfigs / TeleporterFirstParameters")]
    public class TeleporterPlayerSettings : ScriptableObject, IConfigable
    {
        [field: SerializeField] public float LineDistance { get; private set; }
        [field: SerializeField] public float FirstPosition { get; private set; }
        [field: SerializeField] public float SliderSpeed { get; private set; }
        [field: SerializeField] public float MinSpeed { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float TimeToReachMaximumSpeed { get; private set; }
        [field: SerializeField] public float RecoveryTimeAfterCollision { get; private set; }
        [field: SerializeField] public PlayerJumpTeleporter PlayerJumpTeleporter { get; private set; }
    }
}