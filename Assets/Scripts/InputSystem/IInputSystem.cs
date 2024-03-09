using System;

namespace InputSystem
{
    public interface IInputSystem
    {
        Swipe Move();
        void Jump();
    }
}