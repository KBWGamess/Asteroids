using Asteroids.Player;
using Asteroids.Weapons;
using Asteroids.Infrastructure;
using Asteroids.Core;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private BulletView _bulletPrefab;

        public override void InstallBindings()
        {
            Container.Bind<PlayerModel>().AsSingle();

            Container.Bind<KeyboardInput>().AsSingle();
            Container.Bind<MouseInput>().AsSingle();

#if UNITY_ANDROID || UNITY_IOS
            Container.Bind<MobileInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IInputHandler>().To<MobileInput>().FromResolve().AsSingle();
#else
            Container.Bind<MobileInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IInputHandler>().To<CombinedInput>().AsSingle();
#endif

            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerSimulationSystem>().AsSingle();
            Container.Bind<CameraProvider>().AsSingle();

            Container.Bind<ObjectPool<BulletView>>()
                .FromInstance(new ObjectPool<BulletView>(_bulletPrefab, Container))
                .AsSingle();
            Container.Bind<BulletFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}