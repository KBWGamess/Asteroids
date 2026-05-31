using Asteroids.Physics;
using UnityEngine;

namespace Asteroids.Weapons
{
    public class Laser
    {
        public bool IsActive { get; private set; }
        private float _duration = 0.5f;
        private float _timer;

        public Vector2 Origin { get; private set; }
        public Vector2 Direction { get; private set; }

        public void Fire(Vector2 origin, Vector2 direction)
        {
            IsActive = true;
            Origin = origin;
            Direction = direction;
            _timer = _duration;
        }

        public void Tick(float deltaTime)
        {
            if (!IsActive) return;
            _timer -= deltaTime;
            if (_timer <= 0) IsActive = false;
        }

        public void Deactivate() => IsActive = false;
    }
}