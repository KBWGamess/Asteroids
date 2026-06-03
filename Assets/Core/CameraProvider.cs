using UnityEngine;

namespace Asteroids.Core
{
    public class CameraProvider
    {
        private Camera _camera;

        public Camera Camera => _camera ??= Camera.main;
    }
}