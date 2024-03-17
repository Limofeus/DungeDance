using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnfadingArrow : Arrow
{
    SpriteRenderer visualArrowSR;
    public override void starto()
    {
        visualArrowSR = arrowVisual.transform.GetComponent<SpriteRenderer>();
        visualArrowSR.color = new Color(1f, 1f, 1f, 0f);
        base.starto();
    }
    public override void UpdateStuff()
    {
        if (transform.localPosition.x/arrowSpeed < 0.4f && !disabled)
        {
            visualArrowSR.color = new Color(1f, 1f, 1f, Mathf.Lerp(visualArrowSR.color.a, 1f, 8f * Time.deltaTime));
        }
        base.UpdateStuff();
    }
}
