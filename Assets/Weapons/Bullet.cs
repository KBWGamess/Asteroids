using Asteroids.Physics;
using UnityEngine;

namespace Asteroids.Weapons
{
    public class Bullet
    {
        public PhysicsBody Body { get; }
        public bool IsActive { get; private set; }
        private float _lifetime;
        private float _maxLifetime = 2f;

        public Bullet(float speed, Vector2 position, Vector2 direction)
        {
            Body = new PhysicsBody { MaxSpeed = speed, LinearDrag = 1f };
            Body.Position = position;
            Body.SetVelocity(direction * speed);
            IsActive = true;
            _lifetime = 0f;
        }

        public void Tick(float deltaTime)
        {
            if (!IsActive) return;
            _lifetime += deltaTime;
            if (_lifetime >= _maxLifetime) IsActive = false;
            Body.Tick(deltaTime);
        }

        public void Deactivate() => IsActive = false;
    }
}