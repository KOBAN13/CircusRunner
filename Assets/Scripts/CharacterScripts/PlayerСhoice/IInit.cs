using Configs;

namespace Character.PlayerChoise
{
    public interface IInit
    {
        public void Init<T>(T playerSettings);
    }
}