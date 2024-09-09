using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscalingArrow : Arrow
{
    public float unfadeDist = 0.5f;
    public float unfadeRand = 0.07f;
    SpriteRenderer visualArrowSR;
    public override void starto()
    {
        unfadeDist = unfadeDist + Random.Range(-unfadeRand, unfadeRand);
        visualArrowSR = arrowVisual.transform.GetComponent<SpriteRenderer>();
        //visualArrowSR.color = new Color(1f, 1f, 1f, 0f);
        base.starto();
    }
    public override void UpdateStuff()
    {
        if (transform.localPosition.x / arrowSpeed < unfadeDist && !disabled)
        {
            visualArrowSR.color = new Color(1f, 1f, 1f, Mathf.Lerp(visualArrowSR.color.a, 1f, 8f * Time.deltaTime));
            visualArrowSR.transform.localScale = Vector3.Lerp(visualArrowSR.transform.localScale, Vector3.one, 12f * Time.deltaTime);
        }
        base.UpdateStuff();
    }
}
