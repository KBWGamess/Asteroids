using Asteroids.Core;
using UnityEngine;
using Zenject;

namespace Asteroids.Player
{
    public class PlayerSimulationSystem : ITickable
    {
        private readonly PlayerModel _model;
        private readonly WorldBounds _bounds;
        private readonly GameStateManager _stateManager;

        public PlayerSimulationSystem(PlayerModel model, WorldBounds bounds, GameStateManager stateManager)
        {
            _model = model;
            _bounds = bounds;
            _stateManager = stateManager;
        }

        public void Tick()
        {
            if (!_stateManager.IsPlaying) return;
            
            _model.Tick(Time.deltaTime);
            _model.Body.Tick(Time.deltaTime);
            _model.Body.Position = _bounds.Wrap(_model.Body.Position);
        }
    }
}