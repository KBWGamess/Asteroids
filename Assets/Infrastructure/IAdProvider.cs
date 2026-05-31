namespace Asteroids.Infrastructure
{
    public interface IAdProvider
    {
        void ShowInterstitial();
        void ShowRewarded();
        void Initialize();
    }
}