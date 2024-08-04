using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDirAnimatorTriggers : MonoBehaviour, FourDirectionInputTarget
{
    [SerializeField] private Animator targetAnimator;
    [SerializeField] private string[] triggerNames = { "Up", "Right", "Down", "Left" };
    public void OnFourDirInput(FourDirectionInputTarget.FourArrowDir direction)
    {
        Debug.Log($"SettingTrigger! {triggerNames[(int)direction]}");
        targetAnimator.SetTrigger(triggerNames[(int)direction]);
    }
}
