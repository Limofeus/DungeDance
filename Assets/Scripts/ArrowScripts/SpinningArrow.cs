using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningArrow : Arrow
{
    public float rotationSpeed;
    private int reverse;
    Transform visualArrowTrans;
    public override void starto()
    {
        visualArrowTrans = arrowVisual.transform;
        reverse = (Random.Range(0, 2) * -2) + 1;
        //visualArrowTrans.localRotation = Quaternion.Euler(0f, 0f, Random.Range(-3, 4) * 90f);
        base.starto();
    }
    public override void UpdateStuff()
    {
        visualArrowTrans.localRotation = Quaternion.Euler(0f, 0f, transform.localPosition.x * reverse * rotationSpeed);
        base.UpdateStuff();
    }
}
