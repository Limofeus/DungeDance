using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class PointerMover : MonoBehaviour
    {
        [SerializeField] private ShopCell[] _cells;
        [SerializeField] private float _speed = 2;
        [SerializeField] private Vector3 _offset = new(0,1.5f);
        private Vector3 _currentPosition;

        private void OnEnable()
        {
            SetCurrentPosition(0);
            foreach (var cell in _cells)
                cell.OnSelect += SetCurrentPosition;
        }

        private void OnDisable()
        {
            foreach (var cell in _cells)
                cell.OnSelect -= SetCurrentPosition;
        }

        private void SetCurrentPosition(int cellId)
        {
            _currentPosition = _cells[cellId].transform.position + _offset;
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _currentPosition, Time.deltaTime * _speed);
        }
    }
}