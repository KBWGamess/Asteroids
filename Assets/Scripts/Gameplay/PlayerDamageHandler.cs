using Asteroids.Core;
using Asteroids.Player;
using UnityEngine;
using Zenject;

namespace Asteroids.Gameplay
{
    public class PlayerDamageHandler
    {
        private readonly PlayerModel _player;
        private readonly SignalBus _signalBus;

        public PlayerDamageHandler(PlayerModel player, SignalBus signalBus)
        {
            _player = player;
            _signalBus = signalBus;
        }

        public void HandleCollision(Vector2 enemyPosition)
        {
            if (_player.IsDead()) return;
            
            _player.TakeDamage();
            ApplyKnockback(enemyPosition);
            _signalBus.Fire(new OnPlayerHit { RemainingHealth = _player.Health });
            
            if (_player.IsDead())
                _signalBus.Fire(new OnPlayerDied());
        }

        private void ApplyKnockback(Vector2 enemyPosition)
        {
            Vector2 direction = (enemyPosition - _player.Body.Position).normalized;
            _player.Body.SetVelocity(-direction * 3f);
        }
    }
}