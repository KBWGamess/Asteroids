using Asteroids.UI;
using Zenject;

namespace Asteroids.Installers
{
    public class UiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<HudView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverView>().FromComponentInHierarchy().AsSingle();
        }
    }
}