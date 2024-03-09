using System;
using Configs;

namespace Character
{
    public interface IStopMovable
    {
        void OnSubcribeEvent();
        void InvokeEventStopMovements(IUseConfigable config);
        void Dispose();
    }
}