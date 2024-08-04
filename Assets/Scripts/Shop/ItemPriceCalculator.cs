using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemPriceCalculator
{
    public static int CalculateItemPrice(int itemId, SaveData currentPlayerSaveData)
    {
        if (itemId == -3)
            return 1337;
        return (ItemSpriteDictionary.itemRarity[itemId] * 300) + 200; //Placeholder for now
    }
}
