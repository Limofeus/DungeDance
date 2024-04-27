using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotFollower : MonoBehaviour
{
    [SerializeField] private Vector2 _offset;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _speed;

    private void Update()
    {
        var horizontalInput = Mathf.Clamp(Input.mousePosition.x, 0, Screen.width * 0.6f);
        var horizontalMove = _offset.x + Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -_maxDistance,_maxDistance);
        var newPosition = new Vector2(horizontalMove, _offset.y);
        transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * _speed);
    }
}
