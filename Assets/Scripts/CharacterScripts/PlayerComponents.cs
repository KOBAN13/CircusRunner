using UnityEngine;

namespace Character
{
    public class PlayerComponents : MonoBehaviour
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Player Player { get; private set; }
        public float TargetDirectionY { get; set; }
    }
}