using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpScaleVisibilityToggle : MonoBehaviour
{
    public bool isVisible = false;
    [SerializeField] private float lerpPower;

    private Vector3 startScale;

    private void Start()
    {
        startScale = transform.localScale;
        if (!isVisible) transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, isVisible ? startScale : Vector3.zero, Time.deltaTime * lerpPower);
    }

    public void SetVisibility(bool visible)
    {
        isVisible = visible;
    }
}
