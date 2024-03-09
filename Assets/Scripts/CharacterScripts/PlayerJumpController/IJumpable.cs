namespace Character.PlayerJumpController
{
    public interface IJumpable
    {
        void Jump();
        bool IsJump { get; set; }
    }
}