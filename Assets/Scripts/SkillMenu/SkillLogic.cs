using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Skills
{
    public class SkillLogic : MonoBehaviour
    {
        [SerializeField] private Transition[] _transitions;
        [SerializeField] private bool _isActive = false;
        [SerializeField] private GameObject _light;
        private float _difaultScale = 1f; 
        private float _selectedScale = 1.2f; 
        private float _scaleTime = 0.3f; 

        private void Start()
        {
            foreach (var transition in _transitions)
            {
                transition.SetActive(_isActive);
            }
        }
        
        public void SetActive(bool isActive)
        {
            _isActive = isActive;
            foreach (var transition in _transitions)
                transition.SetActive(_isActive);
            _light.SetActive(_isActive);
        }

        private void OnMouseEnter()
        {
            StartCoroutine(ChangeScale(_selectedScale));
        }

        private void OnMouseExit()
        {
            StartCoroutine(ChangeScale(_difaultScale));
        }

        private IEnumerator ChangeScale(float scale)
        {
            var timer = 0f;
            while (timer < _scaleTime)
            {
                transform.localScale = Vector3.one * Mathf.Lerp(transform.localScale.x, scale, timer / _scaleTime);
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

        }
    }
}