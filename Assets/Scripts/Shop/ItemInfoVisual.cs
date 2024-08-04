using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoVisual : MonoBehaviour
{
    [SerializeField] private TextTypeoutAnim itemNameTypeout;
    [SerializeField] private TextTypeoutAnim itemDescTypeout;
    [SerializeField] private GameObject[] rarityObjs;
    private int lastItemId = -1;
    void Start()
    {
        
    }

    public void SetItemInfo(int itemId)
    {
        if(itemId != lastItemId)
        {
            itemNameTypeout.UpdateText(LocalisationSystem.GetLocalizedValue($"item_name_id{itemId}"));
            itemDescTypeout.UpdateText(LocalisationSystem.GetLocalizedValue($"item_desc_id{itemId}"));
            int itemRarity = ItemSpriteDictionary.itemRarity[itemId];
            for(int i = 0; i < rarityObjs.Length; i++)
            {
                rarityObjs[i].SetActive(i == itemRarity);
            }
        }
    }
}
