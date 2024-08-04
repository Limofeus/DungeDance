using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShopSlotButton : MonoBehaviour
{
    public enum SlotState
    {
        Locked = 0,
        ItemAvailable = 1,
        ItemTimeout = 2
    }

    [SerializeField] private int buttonId;
    [SerializeField] private float selectScaleMult;
    [SerializeField] private float selectLerpPow;

    [SerializeField] private Transform selectionScaler;
    [SerializeField] private GameObject[] slotStateGameObjects;

    [SerializeField] private SpriteRenderer itemSprite;
    [SerializeField] private SpriteRenderer[] itemOutlineSprites;

    [SerializeField] private TextMeshPro lowerText;
    [SerializeField] private TextMeshPro restockText;
    [SerializeField] private Color enoughMoneyColor;
    [SerializeField] private Color notEnoughMoneyColor;
    [SerializeField] private Color lockedTimeoutColor;
    [SerializeField] private Color[] rarityColors;

    [SerializeField] private ShopModel shopModel;

    [SerializeField] private float uiLockValueCheck = 0f;

    private SlotState currentSlotState;

    private Vector3 startScale;

    public bool selectable = false;
    private bool selected = false;
    private bool mouseOver = false;
    [HideInInspector] public float restockTime = 0f;

    private void Update()
    {
        if (!selected)
        {
            if(selectable && mouseOver)
            {
                shopModel.OnItemButtonSelect(buttonId);
                selected = true;
            }
        }
        else
        {
            if(!(selectable && mouseOver))
            {
                shopModel.OnItemButtonDeselected(buttonId);
                selected = false;
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    shopModel.OnItemButtonClick(buttonId);
                }
            }
        }
        if(currentSlotState == SlotState.ItemTimeout)
        {
            restockTime -= Time.deltaTime;
            restockText.text = TimeSpan.FromSeconds(restockTime).ToString(@"m\:ss");
            if (restockTime <= 0f)
            {
                shopModel.RestockItem(buttonId);
            }
        }
        selectionScaler.localScale = Vector3.Lerp(selectionScaler.localScale, Vector3.one * (selected ? selectScaleMult : 1f), Time.deltaTime * selectLerpPow);
    }
    public void SetSlotState(SlotState slotState)
    {
        currentSlotState = slotState;
        for(int i = 0; i < slotStateGameObjects.Length; i++)
        {
            slotStateGameObjects[i].SetActive(i == (int)slotState);
        }
        if(slotState != SlotState.ItemAvailable)
        {
            lowerText.color = lockedTimeoutColor;
            if(slotState == SlotState.ItemTimeout)
            {
                lowerText.text = "-";
            }
        }
    }
    
    public void SetSelectability(bool setSelectable)
    {
        selectable = setSelectable;
    }

    public void SetItemData(int itemSpriteId, int itemPrice, bool enoughMoney)
    {
        itemSprite.sprite = ItemSpriteDictionary.itemSprites[itemSpriteId];
        foreach(var itemOutlineSprite in itemOutlineSprites)
        {
            itemOutlineSprite.sprite = ItemSpriteDictionary.itemSprites[itemSpriteId];
            itemOutlineSprite.color = rarityColors[ItemSpriteDictionary.itemRarity[itemSpriteId]];
        }

        lowerText.color = enoughMoney ? enoughMoneyColor : notEnoughMoneyColor;
        lowerText.text = itemPrice.ToString();
    }

    private void OnMouseEnter()
    {
        if (MenuDataManager.uiLockValue > uiLockValueCheck) return;
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        if (MenuDataManager.uiLockValue > uiLockValueCheck) return;
        mouseOver = false;
    }
}
