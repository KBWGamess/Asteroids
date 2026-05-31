using UnityEngine;
using Zenject;
using Asteroids.Player;

namespace Asteroids.Weapons
{
    public class LaserView : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Laser _laser;
        private PlayerModel _player;

        [Inject]
        public void Construct(PlayerModel player)
        {
            _player = player;
            _laser = new Laser();

            _lineRenderer = gameObject.AddComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
            _lineRenderer.positionCount = 2;
            _lineRenderer.sortingOrder = -1;
            _lineRenderer.enabled = false;

            Material mat = new Material(Shader.Find("Sprites/Default"));
            mat.color = Color.red;
            _lineRenderer.material = mat;
        }

        public void Fire()
        {
            if (!_player.UseLaser()) return;

            float rad = _player.Body.Rotation * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
            _laser.Fire(_player.Body.Position, dir);
        }

        public bool IsActive => _laser?.IsActive ?? false;
        public Vector2 Origin => (Vector2)transform.position;
        public Vector2 Direction
        {
            get
            {
                float rad = _player.Body.Rotation * Mathf.Deg2Rad;
                return new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
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
                float rad = _player.Body.Rotation * Mathf.Deg2Rad;
                Vector2 dir = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
                Vector2 end = origin + dir * 50f;
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