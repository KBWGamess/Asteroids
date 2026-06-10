using Asteroids.Core;
using Asteroids.Gameplay;
using Zenject;

namespace Asteroids.Installers
{
    public class GameInstaller : MonoInstaller
    {
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<OnPlayerDied>().OptionalSubscriber();
            Container.DeclareSignal<OnPlayerHit>().OptionalSubscriber();
            Container.DeclareSignal<OnEnemyKilled>().OptionalSubscriber();

            Container.Bind<ScoreService>().AsSingle();
            Container.Bind<RewardService>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyCollisionHandler>().AsSingle();
            Container.Bind<GameStateManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStartup>()
                .FromComponentInHierarchy()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<GameAnalyticsHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverHandler>().AsSingle();
            Container.Bind<PlayerDamageHandler>().AsSingle();
            Container.Bind<CollisionReactionHandler>().AsSingle();
        }
    }
}