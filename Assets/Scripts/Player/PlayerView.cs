using UnityEngine;
using Zenject;

namespace Asteroids.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _invincibilityEffect;
        private PlayerModel _model;

        [Inject]
        public void Construct(PlayerModel model)
        {
            _model = model;
            _model.Body.Position = transform.position;
        }

        private void Update()
        {
            if (_model == null) return;
            transform.position = _model.Body.Position;
            transform.rotation = Quaternion.Euler(0, 0, -_model.Body.Rotation);

            if (_invincibilityEffect != null)
            {
                if (_model.IsInvincible && !_invincibilityEffect.isPlaying)
                    _invincibilityEffect.Play();
                else if (!_model.IsInvincible && _invincibilityEffect.isPlaying)
                    _invincibilityEffect.Stop();
            }
        }
    }
}