using UnityEngine;
using UnityEngine.EventSystems;
using Asteroids.Player;
using Zenject;

namespace Asteroids.UI
{
    public class JoystickView : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _handle;
        [SerializeField] private float _radius = 50f;

        private MobileInput _mobileInput;
        private Vector2 _center;

        [Inject]
        public void Construct(MobileInput mobileInput)
        {
            _mobileInput = mobileInput;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _center = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 delta = eventData.position - _center;
            delta = Vector2.ClampMagnitude(delta, _radius);
            _handle.anchoredPosition = delta;
            _mobileInput.SetJoystick(delta / _radius);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _handle.anchoredPosition = Vector2.zero;
            _mobileInput.SetJoystick(Vector2.zero);
        }
    }
}