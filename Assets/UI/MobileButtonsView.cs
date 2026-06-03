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

            _fireButton.OnHold += () => _mobileInput.SetFire(true);
            _fireButton.OnRelease += () => _mobileInput.SetFire(false);
            _laserButton.OnHold += () => _mobileInput.SetLaser(true);
            _laserButton.OnRelease += () => _mobileInput.SetLaser(false);
        }

        private void OnDestroy()
        {
            if (_mobileInput == null) return;
            _fireButton.OnHold -= () => _mobileInput.SetFire(true);
            _fireButton.OnRelease -= () => _mobileInput.SetFire(false);
            _laserButton.OnHold -= () => _mobileInput.SetLaser(true);
            _laserButton.OnRelease -= () => _mobileInput.SetLaser(false);
        }
    }
}