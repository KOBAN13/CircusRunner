using Ui;
using Zenject;

namespace Installer
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindModel();
        }
        
        private void BindModel()
        {
            Container.BindInterfacesAndSelfTo<Model>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ViewModel>().AsSingle().NonLazy();
        }
    }
}