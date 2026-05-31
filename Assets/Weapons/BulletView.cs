using UnityEngine;

namespace Asteroids.Weapons
{
    public class BulletView : MonoBehaviour
    {
        public Bullet Bullet { get; private set; }

        public void Init(Bullet bullet)
        {
            Bullet = bullet;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            Bullet?.Deactivate();
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Bullet == null || !Bullet.IsActive)
            {
                Deactivate();
                return;
            }
            Bullet.Tick(Time.deltaTime);
            transform.position = Bullet.Body.Position;
        }
    }
}