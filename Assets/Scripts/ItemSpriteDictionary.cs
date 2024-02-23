using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpriteDictionary : MonoBehaviour
{
    public Sprite[] itemSpritesToAssign;
    public int[] itemRarityToAssign; // 0 - common, 1 - rare, 2 - epic, 3 - legendary
    public bool forceAssign;
    public static Sprite[] itemSprites;
    public static int[] itemRarity;
    void Awake()
    {
        if (forceAssign)
        {
            itemSprites = itemSpritesToAssign;
            itemRarity = itemRarityToAssign;
        }
        else
        {
            if (itemSprites == null)
            {
                itemSprites = itemSpritesToAssign;
                //Debug.Log("Assigned");
            }
            if (itemRarity == null)
            {
                itemRarity = itemRarityToAssign;
                //Debug.Log("Assigned");
            }
        }
    }
}
