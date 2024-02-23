using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDistribution : MonoBehaviour
{
    public bool eachFrame;
    public float addFrame;
    public Transform[] buttons;

    private void Start()
    {
        Distribute();
    }
    private void Update()
    {
        if (eachFrame)
            Distribute();
    }
    private void Distribute()
    {
        float upPosition;
        upPosition = 0f;
        foreach(Transform button in buttons)
        {
            upPosition += button.localScale.y + addFrame;
        }
        upPosition = upPosition / 2f;
        for(int i = 0; i < buttons.Length; i++)
        {
            if (i == 0)
                buttons[i].localPosition = Vector3.up * (upPosition - ((buttons[i].localScale.y + addFrame) / 2f));
            else
                buttons[i].localPosition = Vector3.up * (buttons[i-1].localPosition.y - ((buttons[i-1].localScale.y + addFrame) / 2f) - ((buttons[i].localScale.y + addFrame) / 2f));
        }
    }
}
