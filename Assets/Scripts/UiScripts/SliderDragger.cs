using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderDragger : MonoBehaviour
{
    public Sliderd parentSlider;
    private void OnMouseEnter()
    {
        parentSlider.MouseEntered();
    }
    private void OnMouseExit()
    {
        parentSlider.MouseExited();
    }
}
