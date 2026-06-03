namespace Asteroids.Infrastructure
{
    public interface IAnalyticsService
    {
        void Initialize();
        void LogGameStart();
        void LogGameOver(int score);
        void LogEnemyKilled(string enemyType);
    }
}