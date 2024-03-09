using InputSystem;

namespace Character.PlayerJumpController
{
    public interface IMovable
    {
        void Move(Swipe axis);
        float Speed { get; set; }
    }
}