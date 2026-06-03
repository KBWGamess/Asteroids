using UnityEngine;
using TMPro;
using Asteroids.Core;
using Zenject;

namespace Asteroids.UI
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coordinates;
        [SerializeField] private TMP_Text _angle;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _laserCharges;
        [SerializeField] private TMP_Text _laserRecharge;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _health;

        private PlayerViewModel _viewModel;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(PlayerViewModel viewModel, SignalBus signalBus)
        {
            _viewModel = viewModel;
            _signalBus = signalBus;
        }

        private void Start()
        {
            _signalBus.Subscribe<OnPlayerHit>(OnPlayerHit);
            _signalBus.Subscribe<OnEnemyKilled>(OnEnemyKilled);
            UpdateHealth(_viewModel.Health);
            UpdateScore(_viewModel.Score);
        }

        private void Update()
        {
            if (_viewModel == null) return;
            if (_coordinates) _coordinates.text = _viewModel.Coordinates;
            if (_angle) _angle.text = _viewModel.Angle;
            if (_speed) _speed.text = _viewModel.Speed;
            if (_laserCharges) _laserCharges.text = _viewModel.LaserCharges;
            if (_laserRecharge) _laserRecharge.text = _viewModel.LaserRecharge;
        }

        private void OnPlayerHit(OnPlayerHit signal) => UpdateHealth(signal.RemainingHealth);
        private void OnEnemyKilled(OnEnemyKilled signal) => UpdateScore(signal.Score);

        private void UpdateHealth(int health)
        {
            if (_health) _health.text = $"HP: {health}";
        }

        private void UpdateScore(int score)
        {
            if (_score) _score.text = $"Score: {score}";
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnPlayerHit>(OnPlayerHit);
            _signalBus.Unsubscribe<OnEnemyKilled>(OnEnemyKilled);
        }
    }
}