using Asteroids.Core;
using UnityEngine;
using Asteroids.Infrastructure;
using Zenject;

namespace Asteroids.Enemies
{
    public class UFOView : MonoBehaviour, IResettable, IEnemy
    {
        public UFOModel Model { get; private set; }
        private WorldBounds _bounds;
        
        public GameObject GameObject => gameObject;
        public bool IsAlive => Model != null && Model.IsAlive;

        [Inject]
        public void Construct(WorldBounds bounds)
        {
            _bounds = bounds;
        }

        public void SetupModel(UFOModel model)
        {
            Model = model;
            gameObject.SetActive(true);
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