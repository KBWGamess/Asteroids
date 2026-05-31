using UnityEngine;
using Asteroids.Core;
using Zenject;

namespace Asteroids.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _invincibilityEffect;
        private PlayerModel _model;
        private WorldBounds _bounds;

        [Inject]
        public void Construct(PlayerModel model, WorldBounds bounds)
        {
            _model = model;
            _bounds = bounds;
            _model.Body.Position = transform.position;
        }

        private void Update()
        {
            _model.Tick(Time.deltaTime);
            _model.Body.Tick(Time.deltaTime);
            _model.Body.Position = _bounds.Wrap(_model.Body.Position);
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