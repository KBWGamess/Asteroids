using Asteroids.Core;
using UnityEngine;
using Zenject;

namespace Asteroids.Player
{
    public class PlayerController : ITickable
    {
        private readonly PlayerModel _model;
        private readonly IInputHandler _input;
        private readonly GameStateManager _stateManager;

        public PlayerController(PlayerModel model, IInputHandler input, GameStateManager stateManager)
        {
            _model = model;
            _input = input;
            _stateManager = stateManager;
        }

        public void Tick()
        {
            if (!_stateManager.IsPlaying) return;
            if (_model.IsInvincible) return;

            if (_input.Horizontal != 0)
                _model.Rotate(_input.Horizontal, Time.deltaTime);
            else
            {
                float targetRotation = _input.GetTargetRotation();
                if (!float.IsNaN(targetRotation))
                    _model.Body.Rotation = targetRotation;
            }

            if (_input.Vertical > 0)
                _model.Thrust(Time.deltaTime);
        }
    }
}