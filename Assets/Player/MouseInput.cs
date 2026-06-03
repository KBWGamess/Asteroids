using Asteroids.Core;
using UnityEngine;

namespace Asteroids.Player
{
    public class MouseInput : IInputHandler
    {
        private readonly CameraProvider _cameraProvider;
        private readonly PlayerModel _player;

        public MouseInput(PlayerModel player, CameraProvider cameraProvider)
        {
            _player = player;
            _cameraProvider = cameraProvider;
        }

        public float Horizontal => 0f;
        public float Vertical => Input.GetMouseButton(0) ? 1f : 0f;
        public bool Fire => Input.GetMouseButton(1);
        public bool Laser => Input.GetMouseButtonDown(2);

        public float GetTargetRotation()
        {
            Vector3 mouseWorld = _cameraProvider.Camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (Vector2)mouseWorld - _player.Body.Position;
            return Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        }
    }
}