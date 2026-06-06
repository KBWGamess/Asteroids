using Asteroids.Infrastructure;

namespace Asteroids.Weapons
{
    public class LaserFactory
    {
        private readonly PlayerConfig _config;

        public LaserFactory(PlayerConfig config)
        {
            _config = config;
        }

        public Laser Create()
        {
            return new Laser(_config.laserDuration);
        }
    }
}