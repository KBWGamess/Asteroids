using UnityEngine;
using Asteroids.Core;

namespace Asteroids.Enemies
{
    public class AsteroidView : MonoBehaviour
    {
        public AsteroidModel Model { get; private set; }
        private WorldBounds _bounds;

        public void Init(AsteroidModel model, WorldBounds bounds)
        {
            Model = model;
            _bounds = bounds;
            gameObject.SetActive(true);
            float scale = model.Size == AsteroidSize.Large ? 1.5f :
                model.Size == AsteroidSize.Medium ? 0.8f : 0.4f;
            transform.localScale = Vector3.one * scale;
        }

        public void Deactivate()
        {
            Model?.Kill();
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Model == null || !Model.IsAlive) { Deactivate(); return; }
            Model.Tick(Time.deltaTime);
            Model.Body.Position = _bounds.Wrap(Model.Body.Position);
            transform.position = Model.Body.Position;
        }
    }
}