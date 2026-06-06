using Asteroids.Core;
using UnityEngine;
using Zenject;

namespace Asteroids.Player
{
    public class PlayerSimulationSystem : ITickable
    {
        private readonly PlayerModel _model;
        private readonly WorldBounds _bounds;

        public PlayerSimulationSystem(PlayerModel model, WorldBounds bounds)
        {
            _model = model;
            _bounds = bounds;
        }

        public void Tick()
        {
            _model.Tick(Time.deltaTime);
            _model.Body.Tick(Time.deltaTime);
            _model.Body.Position = _bounds.Wrap(_model.Body.Position);
        }
    }
}