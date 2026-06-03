using UnityEngine;

namespace Asteroids.Player
{
    public class KeyboardInput : IInputHandler
    {
        public float Horizontal =>
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ? -1f :
            Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? 1f : 0f;

        public float Vertical =>
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ? 1f : 0f;

        public bool Fire => Input.GetKey(KeyCode.Space);
        public bool Laser => Input.GetKeyDown(KeyCode.E);

        public float GetTargetRotation() => float.NaN;
    }
}