using UnityEngine;

namespace Character
{
    public interface IPlayerSettings
    {
        float TargetDirectionY { get; set; }
        CharacterController CharacterController { get; }
    }
}