using Firebase.Analytics;
using UnityEngine;
using System.Threading.Tasks;

namespace Asteroids.Infrastructure
{
    public class FirebaseAnalyticsService : IAnalyticsService
    {
        private bool _isInitialized = false;

        public void Initialize()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var status = task.Result;
                if (status == Firebase.DependencyStatus.Available)
                {
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                    _isInitialized = true;
                    Debug.Log("Firebase initialized");
                }
                else
                {
                    Debug.LogError($"Firebase error: {status}");
                }
            });
        }

        public void LogGameStart()
        {
            if (!_isInitialized) return;
            FirebaseAnalytics.LogEvent("game_start");
        }

        public void LogGameOver(int score)
        {
            if (!_isInitialized) return;
            FirebaseAnalytics.LogEvent("game_over", new Parameter("score", score));
        }

        public void LogEnemyKilled(string enemyType)
        {
            if (!_isInitialized) return;
            FirebaseAnalytics.LogEvent("enemy_killed", new Parameter("enemy_type", enemyType));
        }
    }
}