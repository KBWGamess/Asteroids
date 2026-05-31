using UnityEngine;
using UnityEngine.UI;
using Asteroids.Player;
using Zenject;

namespace Asteroids.UI
{
    public class MobileButtonsView : MonoBehaviour
    {
        [SerializeField] private Button _fireButton;
        [SerializeField] private Button _laserButton;

        private MobileInput _mobileInput;

        [Inject]
        public void Construct(MobileInput mobileInput)
        {
            _mobileInput = mobileInput;
        }

        private void Start()
        {
            var fireTrigger = _fireButton.gameObject.AddComponent<HoldButton>();
            fireTrigger.OnHold += () => _mobileInput.SetFire(true);
            fireTrigger.OnRelease += () => _mobileInput.SetFire(false);

            _laserButton.onClick.AddListener(() => _mobileInput.SetLaser(true));
        }
    }
}