using Asteroids.Physics;
using UnityEngine;

namespace Asteroids.Enemies
{
    public class UfoModel
    {
        public PhysicsBody Body { get; }
        public bool IsAlive { get; private set; } = true;
        private readonly float _speed;

        public UfoModel(Vector2 position, float speed)
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
            Vector2 dir = (playerPosition - Body.Position).normalized;
            Body.SetVelocity(dir * _speed);
            Body.Tick(deltaTime);
        }

        public void Kill() => IsAlive = false;
    }
}