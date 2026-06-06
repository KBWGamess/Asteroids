using UnityEngine;

namespace Asteroids.Core
{
    public class WorldBounds
    {
        private readonly float _width;
        private readonly float _height;

        public WorldBounds(float width, float height)
        {
            _width = width / 2f;
            _height = height / 2f;
        }

        public Vector2 Wrap(Vector2 position)
        {
            if (position.x > _width) position.x = -_width;
            else if (position.x < -_width) position.x = _width;

            if (position.y > _height) position.y = -_height;
            else if (position.y < -_height) position.y = _height;

            return position;
        }
    }
}