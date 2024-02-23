using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurseWarningUI : MonoBehaviour
{
    public Animator animator;
    public TextMeshPro textMesh;
    public SpriteRenderer flare;
    public SpriteRenderer bgSmokeOne;
    public SpriteRenderer bgSmokeTwo;
    public Color[] flareColors;
    public Color[] textColors;
    public Color[] smokeColors;

    public void UpdateCurse(int curseId,  bool castDecast, string curseNameKey)
    {
        if (castDecast)
        {
            textMesh.text = LocalisationSystem.GetLocalizedValue(curseNameKey);
            textMesh.color = textColors[curseId];
            flare.color = flareColors[curseId];
            bgSmokeOne.color = smokeColors[curseId];
            bgSmokeTwo.color = smokeColors[curseId];
            animator.SetBool("Cursed", true);
        }
        else
        {
            animator.SetBool("Cursed", false);
        }
    }
}
