using UnityEngine;

namespace Asteroids.Player
{
    public class MouseInput : IInputHandler
    {
        private readonly PlayerModel _player;
        private readonly Camera _camera;

        public MouseInput(PlayerModel player)
        {
            _player = player;
            _camera = Camera.main;
        }

        public float Horizontal => 0f;
        public float Vertical => Input.GetMouseButton(0) ? 1f : 0f;
        public bool Fire => Input.GetMouseButton(1);
        public bool Laser => Input.GetMouseButtonDown(2);

        public float GetTargetRotation()
        {
            Vector3 mouseWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = (Vector2)mouseWorld - _player.Body.Position;
            return Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        }
    }
}