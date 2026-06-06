using Asteroids.Enemies;
using Asteroids.Gameplay;
using Asteroids.Infrastructure;
using Asteroids.Core;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private AsteroidView _asteroidPrefab;
        [SerializeField] private UFOView _ufoPrefab;

        public override void InstallBindings()
        {
            Container.Bind<ObjectPool<AsteroidView>>()
                .FromInstance(new ObjectPool<AsteroidView>(_asteroidPrefab, Container))
                .AsSingle();
            Container.Bind<ObjectPool<UFOView>>()
                .FromInstance(new ObjectPool<UFOView>(_ufoPrefab, Container))
                .AsSingle();

            Container.Bind<AsteroidSpawner>().AsSingle();
            Container.Bind<UFOSpawner>().AsSingle();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
            Container.Bind<AsteroidSplitter>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyMovementSystem>().AsSingle();
        }
    }
}