using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyDirectionMiniArrow : Arrow
{
    public override void UpdateStuff()
    {
        transform.localPosition = transform.localPosition + Vector3.left * arrowSpeed * Time.deltaTime;
        if (transform.localPosition.x < -3 && !disabled)
        {
            ArrowHit("POH");
        }
        if (transform.localPosition.x < -15)
            Despawn();
        /*
        if (transform.position.x < 1 && !sent)
        {
            if (Manager.Monster != null)
            {
                Manager.MonsterComp.ArrowThere(this); // If this wont work, ill try lower one
                //Manager.Monster.SendMessage("ArrowThere", this);
                sent = true;
            }
        }
        */
        if (Auto && transform.localPosition.x < 0 && !disabled)
        {
            mainManager.PressThis(direction);
        }
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
                    mainManager.ArrowHit(0f, arrowSpeed, false, transform, true);
                    arrowVisual.Effect(0f / arrowSpeed, false);
                }
                disabled = true;
            }
        }
    }
}
