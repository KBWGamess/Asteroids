using Asteroids.Enemies;
using Asteroids.Core;
using UnityEngine;

namespace Asteroids.Gameplay
{
    public class CollisionReactionHandler
    {
        private readonly RewardService _rewardService;
        private readonly AsteroidSplitter _splitter;
        private readonly PlayerDamageHandler _damageHandler;

        public CollisionReactionHandler(
            RewardService rewardService,
            AsteroidSplitter splitter,
            PlayerDamageHandler damageHandler)
        {
            _rewardService = rewardService;
            _splitter = splitter;
            _damageHandler = damageHandler;
        }

        public void OnBulletHitAsteroid(AsteroidView asteroid)
        {
            _rewardService.GiveReward(asteroid.Model.Size);
            _splitter.Split(asteroid);
        }

        public void OnBulletHitUFO(UFOView ufo)
        {
            _rewardService.GiveReward(EnemyType.UFO);
            ufo.Deactivate();
        }

        public void OnPlayerHitAsteroid(AsteroidView asteroid)
        {
            _damageHandler.HandleCollision(asteroid.Model.Body.Position);
        }

        public void OnPlayerHitUFO(UFOView ufo)
        {
            _damageHandler.HandleCollision(ufo.Model.Body.Position);
        }

        public void OnLaserHitAsteroid(AsteroidView asteroid)
        {
            _rewardService.GiveReward(asteroid.Model.Size);
            asteroid.Deactivate();
        }

        public void OnLaserHitUFO(UFOView ufo)
        {
            _rewardService.GiveReward(EnemyType.UFO);
            ufo.Deactivate();
        }
    }
}