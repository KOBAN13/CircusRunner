using InputSystem;
using UnityEngine;

namespace Character
{
    public interface IControllable
    {
        void Move(Swipe axis);
        void Jump();
    }
}