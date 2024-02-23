using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchItemUse : MonoBehaviour
{
    public int itemNummber;
    public bool dropItem;
    public ItemHolder itemHolder;

    private void OnMouseDown()
    {
        if (!dropItem)
        {
            itemHolder.UseItem(itemNummber);
        }
        else
        {
            itemHolder.DropItem(itemNummber);
        }
    }
}
