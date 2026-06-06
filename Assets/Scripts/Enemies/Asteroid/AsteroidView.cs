using UnityEngine;
using Asteroids.Core;
using Asteroids.Infrastructure;
using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidView : MonoBehaviour, IResettable, IEnemy
    {
        public AsteroidModel Model { get; private set; }
        private WorldBounds _bounds;
        
        public GameObject GameObject => gameObject;
        public bool IsAlive => Model != null && Model.IsAlive;

        [Inject]
        public void Construct(WorldBounds bounds)
        {
            _bounds = bounds;
        }

        public void SetupModel(AsteroidModel model)
        {
            Model = model;
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
            if (Model == null || !Model.IsAlive) Deactivate();
        }
        
        
    }
}