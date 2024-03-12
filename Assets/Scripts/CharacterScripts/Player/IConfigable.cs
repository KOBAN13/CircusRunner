namespace Character
{
    public interface IConfigable
    {
        float MaxSpeed { get; }
        float RecoveryTimeAfterCollision { get; }
    }

    public interface IUseConfigable
    {
        IConfigable Config { get; }
    }
}