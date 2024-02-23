using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePlayerItem : MonoBehaviour
{
    public Transform itemHolder;
    public SpriteRenderer itemSr;
    public SpriteRenderer itemRaritySr;
    public StorageChestManager storageChestManager;
    public Color[] itemRarityColors;
    public GameObject endPrefab;
    private GameObject itemObj;
    private bool mouseOver;
    [HideInInspector]
    public int playerSlotId;
    private int thisItemId = -1;
    private Color thisItemRarityColor;
    private string itemName;
    private string itemDesc;
    private int itemRarity;
    void Awake()
    {
        itemObj = itemHolder.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        itemHolder.localScale = Vector3.Lerp(itemHolder.localScale, Vector3.one * (mouseOver ? 1.1f : 0.95f), 10f * Time.deltaTime);
        itemRaritySr.color = Color.Lerp(itemRaritySr.color, thisItemRarityColor, 15f * Time.deltaTime);
        itemSr.color = Color.Lerp(itemSr.color, Color.white, 15f * Time.deltaTime);
        if (Input.GetMouseButtonDown(0) && mouseOver && thisItemId > -1)
        {
            storageChestManager.StoreItem(playerSlotId);
        }
        if (Input.GetMouseButtonDown(1) && mouseOver && thisItemId > -1)
        {
            storageChestManager.RemovePItem(playerSlotId);
        }
    }
    public void SetAppearing()
    {
        itemHolder.localScale = Vector3.one * 1.5f;
        itemSr.color = new Color(1f, 1f, 1f, 0f);
        itemRaritySr.color = new Color(thisItemRarityColor.r, thisItemRarityColor.g, thisItemRarityColor.b, 0f);
    }
    public void UpdateItem(int itemId)
    {
        thisItemId = itemId;
        if (itemId > -1)
        {
            itemObj.SetActive(true);
            Sprite itemSprite = ItemSpriteDictionary.itemSprites[itemId];
            itemSr.sprite = itemSprite;
            itemRaritySr.sprite = itemSprite;
            itemRarity = ItemSpriteDictionary.itemRarity[itemId];
            thisItemRarityColor = itemRarityColors[itemRarity];
            itemRaritySr.color = thisItemRarityColor;
            itemName = LocalisationSystem.GetLocalizedValue("item_name_id" + itemId.ToString());
            itemDesc = LocalisationSystem.GetLocalizedValue("item_desc_id" + itemId.ToString());
        }
        else
        {
            itemObj.SetActive(false);
        }
    }
    public void SpawnEndObj()
    {
        if (thisItemId > -1)
            Instantiate(endPrefab, transform.position, Quaternion.identity).GetComponent<StorageItemEnd>().UpdateItem(ItemSpriteDictionary.itemSprites[thisItemId], thisItemRarityColor, 1f);
    }
    public void SpawnRemObj()
    {
        if(thisItemId > -1)
            Instantiate(endPrefab, transform.position, Quaternion.identity).GetComponent<StorageItemEnd>().UpdateItem(ItemSpriteDictionary.itemSprites[thisItemId], thisItemRarityColor, 1f, true);
    }
    private void OnMouseEnter()
    {
        mouseOver = true;
        if(itemName != null && itemDesc != null)
        {
            storageChestManager.UpdateItemTexts(itemName, itemDesc, itemRarity);
        }
    }
    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
