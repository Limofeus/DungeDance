using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInfoIcon : MonoBehaviour
{
    public int itemId;
    public int slotId;
    public Color[] rarityColours;
    public GameObject[] numberIcons;
    public SpriteRenderer sprite;
    public SpriteRenderer background;
    public TextMeshPro itemName;
    public TextMeshPro itemDescription;
    public Animator animator;
    public MenuDataManager menuDataManager;
    private bool mouseOver;
    public static bool animatingStart;
    public static bool animating;
    [SerializeField] private float uiLockValueCheck = 0f;

    private void Start()
    {
        sprite.sprite = ItemSpriteDictionary.itemSprites[itemId];
        background.sprite = ItemSpriteDictionary.itemSprites[itemId];
        background.color = rarityColours[ItemSpriteDictionary.itemRarity[itemId]];
        itemName.text = LocalisationSystem.GetLocalizedValue("item_name_id" + itemId.ToString());
        itemDescription.text = LocalisationSystem.GetLocalizedValue("item_desc_id" + itemId.ToString());
        //Debug.Log("item_name_id" + itemId.ToString());
    }
    private void Update()
    {
        if((Input.GetMouseButtonDown(1) || Input.touchCount == 2 ) && mouseOver && !animating)
        {
            animator.SetTrigger("Drop");
            menuDataManager.DropItemBySlotId(slotId);
        }
    }
    private void OnMouseEnter()
    {
        if (MenuDataManager.uiLockValue > uiLockValueCheck) return;
        if (!animatingStart)
            animator.SetBool("MouseOver", true);
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        if (MenuDataManager.uiLockValue > uiLockValueCheck) return;
        animator.SetBool("MouseOver", false);
        mouseOver = false;
    }
    public void LoadItemId(int loadItemId, int tooltipNumber)
    {
        if (loadItemId != -1)
        {
            gameObject.SetActive(true);
            numberIcons[0].SetActive(false);
            numberIcons[1].SetActive(false);
            numberIcons[2].SetActive(false);
            numberIcons[tooltipNumber - 1].SetActive(true);
            itemId = loadItemId;
            sprite.sprite = ItemSpriteDictionary.itemSprites[itemId];
            background.sprite = ItemSpriteDictionary.itemSprites[itemId];
            background.color = rarityColours[ItemSpriteDictionary.itemRarity[itemId]];
            itemName.text = LocalisationSystem.GetLocalizedValue("item_name_id" + itemId.ToString());
            itemDescription.text = LocalisationSystem.GetLocalizedValue("item_desc_id" + itemId.ToString());
        }
        else
            gameObject.SetActive(false);
    }
}
