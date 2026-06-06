using UnityEngine;

namespace Asteroids.Core
{
    public interface IEnemy
    {
        GameObject GameObject { get; }
        bool IsAlive { get; }
        void Deactivate();
    }
}