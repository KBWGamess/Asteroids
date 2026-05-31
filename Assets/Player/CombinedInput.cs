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
        public float Vertical => _keyboard.Vertical > 0 ? _keyboard.Vertical : (Input.GetMouseButton(0) ? 1f : 0f);
        public bool Fire => _keyboard.Fire || Input.GetMouseButton(1);
        public bool Laser => _keyboard.Laser || Input.GetMouseButtonDown(2);
    }
}