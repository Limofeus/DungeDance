using System.Collections;
using UnityEngine;

namespace Skills
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] private Color _difaultColor;
        [SerializeField] private Color _activeColor;
        private SpriteRenderer _spriteRenderer;
        private bool _isActive = true;
        private float _changeTime = 1f;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetActive(bool isActive)
        {
            if (_isActive != isActive)
            {
                _isActive = isActive;
                StartCoroutine(ChangeColor(_isActive? _activeColor :_difaultColor));
            }
        }

        private IEnumerator ChangeColor(Color color)
        {
            var timer = 0f;
            while (timer < _changeTime)
            {
                _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, color, timer / _changeTime);
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

        }
    }
}