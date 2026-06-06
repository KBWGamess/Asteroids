using UnityEngine;

namespace Asteroids.Core
{
    public class CameraProvider
    {
        public Camera Camera { get; }

        public CameraProvider(Camera camera)
        {
            Camera = camera;
        }
    }
}