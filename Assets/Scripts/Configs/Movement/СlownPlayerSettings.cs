using Character;
using Configs;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters Configs", menuName = "CharactersConfigs / ClownFirstParameters")]
public class СlownPlayerSettings : ScriptableObject, IConfigable
{
    [field: SerializeField] public float MinSpeed { get; private set; }
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: SerializeField] public float TimeToReachMaximumSpeed { get; private set; }
    [field: SerializeField] public float RecoveryTimeAfterCollision { get; private set; }
    [field: SerializeField] public int SliderSpeed { get; private set; }
    [field: SerializeField] public PlayerClownJump PlayerClownJump { get; private set; }
}

