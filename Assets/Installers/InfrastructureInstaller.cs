using Asteroids.Infrastructure;
using Asteroids.Core;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
    public class InfrastructureInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ConfigLoader>().AsSingle();

            Container.Bind<PlayerConfig>().FromMethod(ctx =>
                ctx.Container.Resolve<ConfigLoader>().Load<PlayerConfig>(ConfigPaths.Player)
            ).AsSingle();

            Container.Bind<EnemyConfig>().FromMethod(ctx =>
                ctx.Container.Resolve<ConfigLoader>().Load<EnemyConfig>(ConfigPaths.Enemy)
            ).AsSingle();

            Container.Bind<WorldConfig>().FromMethod(ctx =>
                ctx.Container.Resolve<ConfigLoader>().Load<WorldConfig>(ConfigPaths.World)
            ).AsSingle();

            Container.Bind<WorldBounds>().FromMethod(ctx =>
            {
                var config = ctx.Container.Resolve<WorldConfig>();
                return new WorldBounds(config.worldWidth, config.worldHeight);
            }).AsSingle();

            Container.Bind<IAnalyticsService>().To<FirebaseAnalyticsService>().AsSingle();
            Container.Bind<IAdProvider>().To<MockAdProvider>().AsSingle();
        }
    }
}