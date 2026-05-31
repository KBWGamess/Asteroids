using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Asteroids.UI
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private Text _coordinates;
        [SerializeField] private Text _angle;
        [SerializeField] private Text _speed;
        [SerializeField] private Text _laserCharges;
        [SerializeField] private Text _laserRecharge;
        [SerializeField] private Text _score;
        [SerializeField] private Text _health;

        private PlayerViewModel _viewModel;

        [Inject]
        public void Construct(PlayerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Update()
        {
            _coordinates.text = _viewModel.Coordinates;
            _angle.text = _viewModel.Angle;
            _speed.text = _viewModel.Speed;
            _laserCharges.text = _viewModel.LaserCharges;
            _laserRecharge.text = _viewModel.LaserRecharge;
            _score.text = $"Score: {_viewModel.Score}";
            _health.text = $"HP: {_viewModel.Health}";
        }
    }
}