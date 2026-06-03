using UnityEngine;
using Zenject;
using Asteroids.Player;

namespace Asteroids.Weapons
{
    public class LaserView : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Material _laserMaterial;
        
        private Laser _laser;
        private PlayerModel _player;

        [Inject]
        public void Construct(PlayerModel player)
        {
            _player = player;
            _laser = new Laser();

            _lineRenderer.startWidth = 0.05f;
            _lineRenderer.endWidth = 0.05f;
            _lineRenderer.positionCount = 2;
            _lineRenderer.sortingOrder = -1;
            _lineRenderer.material = _laserMaterial;
            _lineRenderer.enabled = false;
        }

        public void Fire()
        {
            if (!_player.UseLaser()) return;
            _laser.Fire();
        }

        public bool IsActive => _laser?.IsActive ?? false;
        public Vector2 Origin => (Vector2)transform.position;
        public Vector2 Direction
        {
            get
            {
                float radians = _player.Body.Rotation * Mathf.Deg2Rad;
                return new Vector2(Mathf.Sin(radians), Mathf.Cos(radians));
            }
        }

        private void Update()
        {
            if (_laser == null) return;
            _laser.Tick(Time.deltaTime);

            if (_laser.IsActive)
            {
                _lineRenderer.enabled = true;
                Vector2 origin = (Vector2)transform.position;
                Vector2 end = origin + Direction * 50f;
                _lineRenderer.SetPosition(0, origin);
                _lineRenderer.SetPosition(1, end);
            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }
    }
}