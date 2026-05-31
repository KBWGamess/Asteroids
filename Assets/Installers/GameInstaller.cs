using UnityEngine;
using Asteroids.Player;
using Asteroids.Core;
using Asteroids.Weapons;
using Asteroids.Enemies;
using Asteroids.Gameplay;
using Asteroids.UI;
using Zenject;

namespace Asteroids.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private BulletView _bulletPrefab;
        [SerializeField] private AsteroidView _asteroidPrefab;
        [SerializeField] private UfoView _ufoPrefab;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<OnPlayerDied>().OptionalSubscriber();
            Container.DeclareSignal<OnPlayerHit>().OptionalSubscriber();
            Container.DeclareSignal<OnEnemyKilled>().OptionalSubscriber();
            Container.DeclareSignal<OnLaserFired>().OptionalSubscriber();
            
            Container.Bind<ConfigLoader>().AsSingle();
            
            Container.Bind<PlayerConfig>().FromMethod(ctx => 
                ctx.Container.Resolve<ConfigLoader>().Load<PlayerConfig>("player.json")
            ).AsSingle();
            
            Container.Bind<EnemyConfig>().FromMethod(ctx => 
                ctx.Container.Resolve<ConfigLoader>().Load<EnemyConfig>("enemy.json")
            ).AsSingle();
            
            Container.Bind<WorldConfig>().FromMethod(ctx => 
                ctx.Container.Resolve<ConfigLoader>().Load<WorldConfig>("world.json")
            ).AsSingle();
            
            Container.Bind<WorldBounds>().FromMethod(ctx =>
            {
                var config = ctx.Container.Resolve<WorldConfig>();
                return new WorldBounds(config.worldWidth, config.worldHeight);
            }).AsSingle();
            
            Container.Bind<PlayerModel>().AsSingle();

            Container.Bind<MobileInput>().FromComponentInHierarchy().AsSingle();
            
            #if UNITY_ANDROID || UNITY_IOS
            Container.Bind<IInputHandler>().To<MobileInput>().FromComponentInHierarchy().AsSingle();
            #else
            Container.Bind<IInputHandler>().To<KeyboardInput>().AsSingle();
            #endif

            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerView>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<ObjectPool<BulletView>>()
                .FromInstance(new ObjectPool<BulletView>(_bulletPrefab))
                .AsSingle();
    
            Container.BindInterfacesAndSelfTo<WeaponController>().AsSingle();
            
            Container.Bind<ObjectPool<AsteroidView>>()
                .FromInstance(new ObjectPool<AsteroidView>(_asteroidPrefab))
                .AsSingle();

            Container.Bind<AsteroidSpawner>().AsSingle();
            
            Container.Bind<ObjectPool<UfoView>>()
                .FromInstance(new ObjectPool<UfoView>(_ufoPrefab))
                .AsSingle();

            Container.Bind<UfoSpawner>().AsSingle();
            
            Container.Bind<ScoreSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CollisionSystem>().AsSingle();
            
            Container.Bind<PlayerViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<HudView>().FromComponentInHierarchy().AsSingle();
            
            Container.BindInterfacesAndSelfTo<LaserView>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            Container.Bind<GameFacade>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameOverView>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<GameBootstrapper>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            Container.Bind<FirebaseAnalyticsService>().AsSingle();
            
            Container.Bind<IAdProvider>().To<MockAdProvider>().AsSingle();
        }
    }
}