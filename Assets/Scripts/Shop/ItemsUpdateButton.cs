using System;
using UnityEngine;

namespace Shop
{
    public class ItemsUpdateButton : MonoBehaviour
    {
        private bool _mouseOnButton;

        public event Action OnClick;
        private void OnMouseEnter()
        {
            _mouseOnButton = true;
        }
        private void OnMouseExit()
        {
            _mouseOnButton = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _mouseOnButton)
                OnClick?.Invoke();
        }
    }
}