namespace Asteroids.Core
{
    public struct OnPlayerDied { }
    
    public struct OnPlayerHit
    {
        public int RemainingHealth;
    }
    
    public struct OnEnemyKilled
    {
        public int Score;
    }
    
}