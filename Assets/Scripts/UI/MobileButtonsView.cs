using UnityEngine;
using Asteroids.Player;
using Zenject;

namespace Asteroids.UI
{
    public class MobileButtonsView : MonoBehaviour
    {
        [SerializeField] private HoldButton _fireButton;
        [SerializeField] private HoldButton _laserButton;

        private MobileInput _mobileInput;

        [Inject]
        public void Construct(MobileInput mobileInput)
        {
            _mobileInput = mobileInput;
        }

        private void Start()
        {
            if (_mobileInput == null) return;
            _fireButton.OnHold += OnFireHold;
            _fireButton.OnRelease += OnFireRelease;
            _laserButton.OnHold += OnLaserHold;
            _laserButton.OnRelease += OnLaserRelease;
        }

        private void OnFireHold() => _mobileInput.SetFire(true);
        private void OnFireRelease() => _mobileInput.SetFire(false);
        private void OnLaserHold() => _mobileInput.SetLaser(true);
        private void OnLaserRelease() => _mobileInput.SetLaser(false);

        private void OnDestroy()
        {
            if (_mobileInput == null) return;
            _fireButton.OnHold -= OnFireHold;
            _fireButton.OnRelease -= OnFireRelease;
            _laserButton.OnHold -= OnLaserHold;
            _laserButton.OnRelease -= OnLaserRelease;
        }
    }
}