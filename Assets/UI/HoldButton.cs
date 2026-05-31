using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Asteroids.UI
{
    public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action OnHold;
        public event Action OnRelease;

        public void OnPointerDown(PointerEventData eventData) => OnHold?.Invoke();
        public void OnPointerUp(PointerEventData eventData) => OnRelease?.Invoke();
    }
}