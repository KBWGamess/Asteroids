using Asteroids.Gameplay;
using UnityEngine;
using Zenject;

namespace Asteroids.Gameplay
{
    public class GameStartup : MonoBehaviour
    {
        private GameFacade _facade;

        [Inject]
        public void Construct(GameFacade facade)
        {
            _facade = facade;
        }

        private void Start()
        {
            _facade.StartGame();
        }
    }
}