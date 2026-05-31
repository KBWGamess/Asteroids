using UnityEngine;
using Zenject;

namespace Asteroids.Player
{
    public class PlayerController : ITickable
    {
        private readonly PlayerModel _model;
        private readonly IInputHandler _input;
        private readonly MouseInput _mouseInput;

        public PlayerController(PlayerModel model, IInputHandler input, MouseInput mouseInput)
        {
            _model = model;
            _input = input;
            _mouseInput = mouseInput;
        }

        public void Tick()
        {
            if (_model.IsInvincible) return;

            if (_input.Horizontal != 0)
                _model.Rotate(_input.Horizontal, Time.deltaTime);
            else
                _model.Body.Rotation = _mouseInput.GetTargetRotation();

            if (_input.Vertical > 0)
                _model.Thrust(Time.deltaTime);
        }
    }
}