using UnityEngine;
using UnityEngine.UI;
using Asteroids.Core;
using Zenject;
using Asteroids.Gameplay;

namespace Asteroids.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Text _finalScore;
        [SerializeField] private Button _restartButton;

        private SignalBus _signalBus;
        private ScoreSystem _scoreSystem;

        [Inject]
        public void Construct(SignalBus signalBus, Asteroids.Gameplay.ScoreSystem scoreSystem)
        {
            _signalBus = signalBus;
            _scoreSystem = scoreSystem;
        }

        private void Start()
        {
            _panel.SetActive(false);
            _signalBus.Subscribe<OnPlayerDied>(ShowGameOver);
            _restartButton.onClick.AddListener(Restart);
        }

        private void ShowGameOver()
        {
            _panel.SetActive(true);
            _finalScore.text = $"Score: {_scoreSystem.Score}";
        }

        private void Restart()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnPlayerDied>(ShowGameOver);
        }
    }
}