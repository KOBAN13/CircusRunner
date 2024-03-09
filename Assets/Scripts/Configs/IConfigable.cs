namespace Configs
{
    public interface IConfigable
    {
        float MinSpeed { get; }
        float MaxSpeed { get; }
        float TimeToReachMaximumSpeed { get; }
        float RecoveryTimeAfterCollision { get; }
    }
}