using Asteroids.Player;
using Asteroids.Gameplay;
using UnityEngine;

namespace Asteroids.UI
{
    public class PlayerViewModel
    {
        private readonly PlayerModel _player;
        private readonly ScoreSystem _score;

        public PlayerViewModel(PlayerModel player, ScoreSystem score)
        {
            _player = player;
            _score = score;
        }

        public string Coordinates => 
            $"X: {_player.Body.Position.x:F1} Y: {_player.Body.Position.y:F1}";
        
        public string Angle => 
            $"Angle: {_player.Body.Rotation % 360:F1}°";
        
        public string Speed => 
            $"Speed: {_player.Body.Velocity.magnitude:F1}";
        
        public string LaserCharges => 
            $"Laser: {_player.LaserCharges}";
        
        public string LaserRecharge => 
            $"Recharge: {_player.LaserRechargeTimer:F1}s";
        
        public int Health => _player.Health;
        
        public int Score => _score.Score;
    }
}