using Asteroids.Core;
using UnityEngine;

namespace Asteroids.Enemies
{
    public class UfoView : MonoBehaviour
    {
        public UfoModel Model { get; private set; }
        private WorldBounds _bounds;

        public void Init(UfoModel model, WorldBounds bounds)
        {
            Model = model;
            _bounds = bounds;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            Model?.Kill();
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Model == null || !Model.IsAlive) { Deactivate(); return; }
            transform.position = Model.Body.Position;
            Model.Body.Position = _bounds.Wrap(Model.Body.Position);
        }
    }
}