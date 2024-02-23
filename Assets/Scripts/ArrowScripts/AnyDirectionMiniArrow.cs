using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyDirectionMiniArrow : Arrow
{
    public override void UpdateStuff()
    {
        transform.localPosition = transform.localPosition + Vector3.left * Speed * Time.deltaTime;
        if (transform.localPosition.x < -3 && !disabled)
        {
            Yes("POH");
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
            Manager.PressThis(direction);
        }
    }
    public override void Yes(string Direction)
    {
        if (Manager.Monster != null)
        {
            if (!disabled)
            {
                if (Direction != "POH")
                {
                    float offset = Mathf.Abs(transform.localPosition.x);
                    Manager.ArrowHit(offset, Speed, true, transform);
                    arrowVisual.Effect(offset / Speed, true);
                }
                else
                {
                    Manager.ArrowHit(0f, Speed, false, transform, true);
                    arrowVisual.Effect(0f / Speed, false);
                }
                disabled = true;
            }
        }
    }
}
