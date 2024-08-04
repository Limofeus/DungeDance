using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeepVisual : MonoBehaviour
{
    [SerializeField] private Transform shopKeepHandTrans;

    [SerializeField] private float handLerpPower;
    [SerializeField] private Vector3 firstItemPos;
    [SerializeField] private float perItemPosAdd;

    [SerializeField] private float boughtTimePenalty;
    [SerializeField] private float secondsToHideHand;

    private float timeSinceDeselect = 0f;
    private bool itemSelected = false;
    private int selectedItemNum = 0;

    private bool handShown;

    private void Update()
    {
        HandMove();
        if (!itemSelected)
        {
            timeSinceDeselect += Time.deltaTime;
            if(timeSinceDeselect >= secondsToHideHand)
            {
                handShown = false;
            }
        }
        HandScale();
    }

    private void HandMove()
    {
        shopKeepHandTrans.position = Vector3.Lerp(shopKeepHandTrans.position,
            firstItemPos + (Vector3.right * perItemPosAdd * selectedItemNum),
            Time.deltaTime * handLerpPower);
    }

    private void HandScale()
    {
        shopKeepHandTrans.localScale = Vector3.Lerp(shopKeepHandTrans.localScale, handShown ? Vector3.one : Vector3.zero, Time.deltaTime * handLerpPower);
    }

    public void UpdateSelectedItem(int newSelectedItemNum)
    {
        if(newSelectedItemNum < 0)
        {
            itemSelected = false;
            if(newSelectedItemNum == -2)
            {
                timeSinceDeselect += boughtTimePenalty;
            }
        }
        else
        {
            handShown = true;
            itemSelected = true;
            selectedItemNum = newSelectedItemNum;
            timeSinceDeselect = 0f;
        }
    }
}
