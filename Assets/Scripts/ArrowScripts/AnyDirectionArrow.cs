using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyDirectionArrow : Arrow
{
    public override void ArrowHit(string Direction)
    {
        if (mainManager.Monster != null)
        {
            if (!disabled)
            {
                if(Direction != "POH")
                {
                    float offset = Mathf.Abs(transform.localPosition.x);
                    mainManager.ArrowHit(offset, arrowSpeed, true, transform);
                    arrowVisual.Effect(offset / arrowSpeed, true);
                }
                else
                {
                    mainManager.ArrowHit(0f, arrowSpeed, false, transform);
                    arrowVisual.Effect(0f / arrowSpeed, false);
                }
                disabled = true;
            }
        }
    }
}
