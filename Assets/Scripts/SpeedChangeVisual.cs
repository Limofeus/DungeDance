using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChangeVisual : MonoBehaviour
{
    public Animator animator;

    public void Popup()
    {
        animator.SetTrigger("Popup");
    }
}
