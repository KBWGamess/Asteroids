using Asteroids.Physics;
using UnityEngine;

namespace Asteroids.Enemies
{
    public class UFOModel
    {
        public PhysicsBody Body { get; }
        public bool IsAlive { get; private set; } = true;
        private readonly float _speed;

        public UFOModel(Vector2 position, float speed)
        {
            Body = new PhysicsBody
            {
                MaxSpeed = speed,
                LinearDrag = 1f,
                Position = position
            };
            _speed = speed;
        }

        public void Tick(float deltaTime, Vector2 playerPosition)
        {
            Vector2 direction = (playerPosition - Body.Position).normalized;
            Body.SetVelocity(direction * _speed);
            Body.Tick(deltaTime);
        }

        public void Kill() => IsAlive = false;
    }
}