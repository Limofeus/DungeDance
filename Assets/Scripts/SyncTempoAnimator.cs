using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTempoAnimator : MonoBehaviour
{
    public Animator animator;
    public bool affectCoolTextTime;
    public static float speedForAnimator;
    void Start()
    {
        animator.speed = speedForAnimator;
        if (affectCoolTextTime)
            GetComponent<CoolText>().TimToRemove = 1f / speedForAnimator;
    }
}
