using System;
using Configs;

namespace Character.PlayerChoise
{
    public interface IVisitor
    {
        void VisitConf(СlownPlayerSettings config);
        void VisitConf(TeleporterPlayerSettings config);
        void Visit(Type config);
    }
}