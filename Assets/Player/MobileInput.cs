using UnityEngine;

namespace Asteroids.Player
{
    public class MobileInput : MonoBehaviour, IInputHandler
    {
        private Vector2 _joystickDelta;
        private bool _fire;
        private bool _laser;

        public float Horizontal => _joystickDelta.x;
        public float Vertical => _joystickDelta.y;
        public bool Fire => _fire;
        public bool Laser => _laser;

        public void SetJoystick(Vector2 delta)
        {
            _joystickDelta = delta;
        }

        public void SetFire(bool value) => _fire = value;
        public void SetLaser(bool value)
        {
            _laser = value;
        }
    }
}