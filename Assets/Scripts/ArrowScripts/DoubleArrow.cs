using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleArrow : Arrow
{
    public GameObject miniArrowPrefab;
    private GameObject miniArrow;
    private Arrow miniArrowComponent;
    public override void starto()
    {
        //Debug.Log("DAT: " + ((transform.position.x + ((Manager.TimeBetweenBeats * Speed) / 2f)) / Manager.AroowSpeed).ToString());
        //Debug.Log("RTM: " + Manager.RTime.ToString());
        //bool instatiateMini = Manager.RTime > (transform.position.x + ((Manager.TimeBetweenBeats * Speed) / 2f)) / Manager.AroowSpeed; <- Spent so much time on this shit
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
        if(instatiateMini)
            miniArrowComponent.starto();
    }
}
