using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowShowAndHide : MonoBehaviour
{
    [SerializeField] private float showScaleMult = 1f;
    [SerializeField] private float lerpPow = 50f;
    private bool shown = false;

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, shown ? showScaleMult * Vector3.one : Vector3.zero, Time.deltaTime * lerpPow);
    }

    public void ShowHideToggle()
    {
        shown = !shown;
    }
}
