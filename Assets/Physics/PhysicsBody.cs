using UnityEngine;

namespace Asteroids.Physics
{
    public class PhysicsBody
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; private set; }
        public float Rotation { get; set; }
        public float MaxSpeed { get; set; } = 10f;
        public float LinearDrag { get; set; } = 0.99f;

        public void AddForce(Vector2 force)
        {
            Velocity += force;
            if (Velocity.magnitude > MaxSpeed)
                Velocity = Velocity.normalized * MaxSpeed;
        }

        public void Tick(float deltaTime)
        {
            Velocity *= Mathf.Pow(LinearDrag, deltaTime);
            Position += Velocity * deltaTime;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Velocity = velocity;
        }
    }
}