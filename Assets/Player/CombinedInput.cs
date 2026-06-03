using UnityEngine;

namespace Asteroids.Player
{
    public class CombinedInput : IInputHandler
    {
        private readonly KeyboardInput _keyboard;
        private readonly MouseInput _mouse;

        public CombinedInput(KeyboardInput keyboard, MouseInput mouse)
        {
            _keyboard = keyboard;
            _mouse = mouse;
        }

        public float Horizontal => _keyboard.Horizontal;
        public float Vertical => _keyboard.Vertical > 0 ? _keyboard.Vertical : _mouse.Vertical;
        public bool Fire => _keyboard.Fire || _mouse.Fire;
        public bool Laser => _keyboard.Laser || _mouse.Laser;
        public float GetTargetRotation() => _mouse.GetTargetRotation();
    }
}