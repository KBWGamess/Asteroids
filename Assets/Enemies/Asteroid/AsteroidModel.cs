using Asteroids.Physics;
using UnityEngine;

namespace Asteroids.Enemies
{
    public enum AsteroidSize { Large, Medium, Small }

    public class AsteroidModel
    {
        public PhysicsBody Body { get; }
        public AsteroidSize Size { get; }
        public bool IsAlive { get; private set; } = true;

        public AsteroidModel(Vector2 position, Vector2 direction, float speed, AsteroidSize size)
        {
            Size = size;
            float radius = size == AsteroidSize.Large ? 1.5f :
                size == AsteroidSize.Medium ? 0.8f : 0.4f;

            Body = new PhysicsBody
            {
                MaxSpeed = speed,
                LinearDrag = 1f,
                Position = position
            };
            Body.SetVelocity(direction.normalized * speed);
        }

        public void Tick(float deltaTime) => Body.Tick(deltaTime);

        public void Kill() => IsAlive = false;
    }
}