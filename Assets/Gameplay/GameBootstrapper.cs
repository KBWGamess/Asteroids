using Asteroids.Core;
using Asteroids.Enemies;
using Asteroids.Player;
using Asteroids.Gameplay;
using UnityEngine;
using Asteroids.Infrastructure;
using Zenject;

namespace Asteroids.Gameplay
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameFacade _facade;
        private UfoSpawner _ufoSpawner;
        private PlayerModel _playerModel;
        private GameStateMachine _stateMachine;
        private SignalBus _signalBus;
        private FirebaseAnalyticsService _analytics;
        private IAdProvider _adProvider;

        [Inject]
        public void Construct(GameFacade facade, UfoSpawner ufoSpawner,
            PlayerModel playerModel, GameStateMachine stateMachine, 
            SignalBus signalBus, FirebaseAnalyticsService analytics,
            IAdProvider adProvider)
        {
            _facade = facade;
            _ufoSpawner = ufoSpawner;
            _playerModel = playerModel;
            _stateMachine = stateMachine;
            _signalBus = signalBus;
            _analytics = analytics;
            _adProvider = adProvider;
        }

        private void Start()
        {
            _analytics.Initialize();
            _analytics.LogGameStart();
            _facade.StartGame();
            _signalBus.Subscribe<OnPlayerDied>(OnPlayerDied);
        }

        private void OnPlayerDied()
        {
            _analytics.LogGameOver(_facade.GetScore());
            _stateMachine.SetGameOver();
            _facade.StopGame();
            _adProvider.ShowInterstitial();
        }

        private void Update()
        {
            if (_ufoSpawner == null || _playerModel == null) return;
            _ufoSpawner.Tick(_playerModel.Body.Position);
        }

        private void OnDestroy()
        {
            _facade.StopGame();
            _signalBus.Unsubscribe<OnPlayerDied>(OnPlayerDied);
        }
    }
}