using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingArrow : Arrow
{
    Transform visualArrowTrans;
    public override void starto()
    {
        visualArrowTrans = arrowVisual.transform;
        visualArrowTrans.localRotation = Quaternion.Euler(0f, 0f, Random.Range(-3, 4) * 90f);
        base.starto();
    }
    public override void UpdateStuff()
    {
        if (transform.localPosition.x / arrowSpeed < 0.4f)
        {
            visualArrowTrans.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(visualArrowTrans.localRotation.eulerAngles.z, 0f, Time.deltaTime * 18f));
        }
        base.UpdateStuff();
    }
}
