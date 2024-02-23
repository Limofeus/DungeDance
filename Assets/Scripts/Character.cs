using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator ChrAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveLocation(bool sten)
    {
        if (sten)
            ChrAnimator.SetTrigger("LocationChangeT");
        ChrAnimator.SetBool("LocationChange", sten);
    }

    public void Init(float TBB)
    {
        ChrAnimator.speed = 1/TBB;
    }

    public void Die()
    {
        Debug.Log("Dying");
        ChrAnimator.SetTrigger("DieT");
        ChrAnimator.SetBool("Die", true);
    }

    public void Press(string Direction)
    {
        if (Direction == "U")
            ChrAnimator.SetTrigger("Jump");
        if (Direction == "R")
            ChrAnimator.SetTrigger("Right");
        if (Direction == "L")
            ChrAnimator.SetTrigger("Left");
        if (Direction == "D")
            ChrAnimator.SetTrigger("Down");
    }
}
