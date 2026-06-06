using UnityEngine;
using Zenject;

namespace Asteroids.Player
{
    public class PlayerController : ITickable
    {
        private readonly PlayerModel _model;
        private readonly IInputHandler _input;

        public PlayerController(PlayerModel model, IInputHandler input)
        {
            _model = model;
            _input = input;
        }

        public void Tick()
        {
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