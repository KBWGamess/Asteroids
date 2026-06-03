using System;

namespace Asteroids.Infrastructure
{
    [Serializable]
    public class PlayerConfig
    {
        public float moveSpeed = 5f;
        public float rotationSpeed = 180f;
        public float drag = 0.98f;
        public float maxSpeed = 10f;
        public int maxHealth = 3;
        public float bulletSpeed = 15f;
        public float fireRate = 0.2f;
        public int maxLaserCharges = 3;
        public float laserRechargeTime = 5f;
        public float invincibilityDuration = 3f;
        public float playerRadius = 0.3f;
        public float bulletRadius = 0.05f;
        public float laserRadius = 0.05f;
        public float bulletLifetime = 2f;
    }
}