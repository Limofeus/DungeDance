using UnityEngine;

namespace Shop
{
    public class Follower : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.position = Vector2.Lerp(transform.position, _pivot.position, Time.deltaTime * _speed);
        }
    }
}
