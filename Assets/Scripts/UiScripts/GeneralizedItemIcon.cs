using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UIElements;
using TMPro;

public class GeneralizedItemIcon : MonoBehaviour
{
    [SerializeField] private UnityEvent onClickEvent;
    [SerializeField] private Color[] rarityColours;
    [SerializeField] private GameObject[] numberIcons;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private TextMeshPro itemName;
    [SerializeField] private TextMeshPro itemDescription;
    [SerializeField] private TextMeshPro downTextObj;
    [SerializeField] private Animator animator;
    private bool mouseOver;
    [HideInInspector] public bool isShown = true;
    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.touchCount == 1) && mouseOver)
        {
            Debug.Log("Item clicked");
            onClickEvent.Invoke();
        }
    }
    public void UpdateItem(int itemId, int slotId = -1, string downText = "")
    {
        sprite.sprite = ItemSpriteDictionary.itemSprites[itemId];
        background.sprite = ItemSpriteDictionary.itemSprites[itemId];
        background.color = rarityColours[ItemSpriteDictionary.itemRarity[itemId]];
        itemName.text = LocalisationSystem.GetLocalizedValue("item_name_id" + itemId.ToString());
        itemDescription.text = LocalisationSystem.GetLocalizedValue("item_desc_id" + itemId.ToString());
        downTextObj.text = downText;
        if(slotId >= 0)
        {
            numberIcons[slotId].SetActive(true);
        }
    }

    public void HideShowItem(bool shown = true)
    {
        isShown = shown;
        animator.SetBool("Hidden", !shown);
    }
    private void OnMouseEnter()
    {
        animator.SetBool("MouseOver", true);
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        animator.SetBool("MouseOver", false);
        mouseOver = false;
    }
}
