namespace Asteroids.Weapons
{
    public class Laser
    {
        public bool IsActive { get; private set; }
        private float _duration = 0.5f;
        private float _timer;

        public void Fire()
        {
            IsActive = true;
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