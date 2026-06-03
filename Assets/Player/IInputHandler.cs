namespace Asteroids.Player
{
    public interface IInputHandler
    {
        float Horizontal { get; }
        float Vertical { get; }
        bool Fire { get; }
        bool Laser { get; }
        float GetTargetRotation();
    }
}