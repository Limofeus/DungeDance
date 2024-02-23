using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyDirectionArrow : Arrow
{
    public override void Yes(string Direction)
    {
        if (Manager.Monster != null)
        {
            if (!disabled)
            {
                if(Direction != "POH")
                {
                    float offset = Mathf.Abs(transform.localPosition.x);
                    Manager.ArrowHit(offset, Speed, true, transform);
                    arrowVisual.Effect(offset / Speed, true);
                }
                else
                {
                    Manager.ArrowHit(0f, Speed, false, transform);
                    arrowVisual.Effect(0f / Speed, false);
                }
                disabled = true;
            }
        }
    }
}
