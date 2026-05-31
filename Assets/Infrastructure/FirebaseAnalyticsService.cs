using Firebase.Analytics;
using UnityEngine;

namespace Asteroids.Infrastructure
{
    public class FirebaseAnalyticsService
    {
        public void Initialize()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var status = task.Result;
                if (status == Firebase.DependencyStatus.Available)
                {
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
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
            FirebaseAnalytics.LogEvent("game_start");
        }

        public void LogGameOver(int score)
        {
            FirebaseAnalytics.LogEvent("game_over",
                new Parameter("score", score));
        }

        public void LogEnemyKilled(string enemyType)
        {
            FirebaseAnalytics.LogEvent("enemy_killed",
                new Parameter("enemy_type", enemyType));
        }
    }
}