using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAnyDirArrow : Arrow
{
    public GameObject miniArrowPrefab;
    private GameObject miniArrow;
    private Arrow miniArrowComponent;
    public override void starto()
    {
        bool instatiateMini = !lastArrow;
        //Debug.Log(instatiateMini);
        if (instatiateMini)
        {
            miniArrow = Instantiate(miniArrowPrefab, transform.position + (transform.right * (mainManager.timeBetweenBeats * arrowSpeed / 2f)), transform.rotation, transform.parent);
            miniArrowComponent = miniArrow.GetComponent<Arrow>();
            miniArrowComponent.arrowSpeed = arrowSpeed;
            miniArrowComponent.Auto = Auto;
            miniArrowComponent.mainManager = mainManager;
        }
        base.starto();
        if (instatiateMini)
            miniArrowComponent.starto();
    }

    public override void ArrowHit(string Direction)
    {
        if (mainManager.Monster != null)
        {
            if (!disabled)
            {
                if (Direction != "POH")
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
