using System;
using UnityEngine;

namespace Shop
{
    public class ItemsUpdateButton : MonoBehaviour
    {
        private bool _mouseOnButton;
        private Animator _animator;

        private const string _isSelect = "IsSelect";

        public event Action OnClick;
        public event Action OnSelect;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnMouseEnter()
        {
            _animator.SetBool(_isSelect, true);
            _mouseOnButton = true;
            OnSelect?.Invoke();
        }

        private void OnMouseExit()
        {
            _animator.SetBool(_isSelect, false);
            _mouseOnButton = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _mouseOnButton)
                OnClick?.Invoke();
        }
    }
}