using UnityEngine;

namespace Asteroids.Infrastructure
{
    public class MockAdProvider : IAdProvider
    {
        public void Initialize()
        {
            Debug.Log("Ad provider initialized");
        }

        public void ShowInterstitial()
        {
            Debug.Log("Showing interstitial ad");
        }

        public void ShowRewarded()
        {
            Debug.Log("Showing rewarded ad");
        }
    }
}