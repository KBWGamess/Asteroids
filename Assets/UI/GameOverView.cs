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
        private ScoreService _scoreService;

        [Inject]
        public void Construct(SignalBus signalBus, Asteroids.Gameplay.ScoreService scoreService)
        {
            _signalBus = signalBus;
            _scoreService = scoreService;
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
            _finalScore.text = $"Score: {_scoreService.Score}";
        }

        private void Restart()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.Game);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnPlayerDied>(ShowGameOver);
            _restartButton.onClick.RemoveListener(Restart);
        }
    }
}