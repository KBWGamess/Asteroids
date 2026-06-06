using Asteroids.Infrastructure;
using Asteroids.Physics;
using UnityEngine;

namespace Asteroids.Player
{
    public class PlayerModel
    {
        public PhysicsBody Body { get; }
        public int Health { get; private set; }
        public float RotationSpeed { get; }
        public float MoveSpeed { get; }
        public int LaserCharges { get; private set; }
        public float LaserRechargeTime { get; }
        public bool IsInvincible { get; private set; }
        private float _invincibilityTimer;
        private readonly float _invincibilityDuration;
        private float _laserRechargeTimer;
        private readonly int _maxLaserCharges;
        public float LaserRechargeTimer => _laserRechargeTimer;

        public PlayerModel(PlayerConfig config)
        {
            Body = new PhysicsBody
            {
                MaxSpeed = config.maxSpeed,
                LinearDrag = config.drag
            };
            Health = config.maxHealth;
            RotationSpeed = config.rotationSpeed;
            MoveSpeed = config.moveSpeed;
            LaserCharges = config.maxLaserCharges;
            LaserRechargeTime = config.laserRechargeTime;
            _invincibilityDuration = config.invincibilityDuration;
            _maxLaserCharges = config.maxLaserCharges;
        }

        public void Rotate(float direction, float deltaTime)
        {
            Body.Rotation += direction * RotationSpeed * deltaTime;
        }

        public void Thrust(float deltaTime)
        {
            float radians = Body.Rotation * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Sin(radians), Mathf.Cos(radians));
            Body.AddForce(direction * MoveSpeed * deltaTime);
        }

        public void TakeDamage()
        {
            if (IsInvincible) return;
            Health--;
            IsInvincible = true;
            _invincibilityTimer = _invincibilityDuration;
        }

        public void SetInvincible(bool value)
        {
            IsInvincible = value;
        }

        public bool UseLaser()
        {
            if (LaserCharges <= 0) return false;
            LaserCharges--;
            return true;
        }

        public void AddLaserCharge()
        {
            LaserCharges++;
        }

        public bool IsDead() => Health <= 0;

        public void Tick(float deltaTime)
        {
            if (IsInvincible)
            {
                _invincibilityTimer -= deltaTime;
                if (_invincibilityTimer <= 0)
                    IsInvincible = false;
            }

            if (LaserCharges < _maxLaserCharges)
            {
                _laserRechargeTimer += deltaTime;
                if (_laserRechargeTimer >= LaserRechargeTime)
                {
                    LaserCharges++;
                    _laserRechargeTimer = 0f;
                }
            }
        }
    }
}