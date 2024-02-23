using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowVisual : MonoBehaviour
{
    public SpriteRenderer SR;
    public SpriteRenderer sA;
    public SpriteRenderer EffectSR;
    public Animator arrowAnimator;
    public Material Effect1;
    public Material Effect2;

    public void Effect(float offset, bool rightdir)
    {
        float maxOffset = 0.1f;
        if (rightdir)
        {
            if (offset < 0.1f)
            {
                float MakeYellow;
                MakeYellow = (0.5f - Mathf.Abs((offset / maxOffset) - 0.5f));
                Color NeededColor = new Color((offset / maxOffset) + MakeYellow, (1f - (offset / maxOffset)) + MakeYellow, 0f);
                SR.color = NeededColor;
                sA.color = NeededColor;
                EffectSR.color = Color.Lerp(EffectSR.color, NeededColor, 0.7f);
                arrowAnimator.SetTrigger("Hit");
            }
            else
            {
                SR.color = Color.red;
                sA.color = Color.red;
            }
        }
        else
        {
            SR.color = Color.red;
            sA.color = Color.red;
        }
    }

    public void AddCurse(int CurseId)
    {
        //Debug.Log("CURSEDARROW");
        switch (CurseId)
        {
            case 0:
                EffectSR.material = Effect1;
                EffectSR.color = new Color(0.5f, 0f, 1f, 0.8f);
                break;
            case 1:
                EffectSR.material = Effect2;
                EffectSR.color = new Color(1f, 0.7f, 0f, 0.8f);
                break;
            default:
                Debug.Log("Lol, there is no such a curse");
                break;
        }
    }
}