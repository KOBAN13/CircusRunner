using Character.PlayerJumpController;

namespace Character
{
    public interface IMovement
    {
        IMovable Movable{ get; }
        IJumpable Jumpable { get; }
    }
}