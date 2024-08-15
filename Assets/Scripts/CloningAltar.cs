using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using TMPro;

public class CloningAltar : MonoBehaviour
{
    [SerializeField] private GeneralizedItemIcon onAltarItem;
    [SerializeField] private GeneralizedItemIcon[] playerItemIcons;
    [SerializeField] private CloneAltarButton cloneAltarButton;
    [SerializeField] private TextMeshPro playerMoneyLabel;
    [SerializeField] private TextMeshPro playerStorageSpaceLabel;
    [SerializeField] private TextMeshPro cloneHintText;

    [SerializeField] private Transform animStartTransform;
    [SerializeField] private Transform animEndTransform;
    [SerializeField] private GameObject cloneEffectPrefab;

    private int selectedItemSlot = 0; // (STARTS FROM 1!!) 0 - none, 1 - 1, 2 - 2, etc.
    private int selectedItemId = -1;
    public SaveData saveData;
    void Start()
    {
        if (MenuDataManager.saveData != null)
            saveData = MenuDataManager.saveData;
        else
            saveData = SaveSystem.Load();

        onAltarItem.HideShowItem(false);
        UpdateItemsFromSaveData();
        UpdateMoneySlotsLabels();

        playerItemIcons[0].UpdateItem(saveData.item1Id, 0, LocalisationSystem.GetLocalizedValue("ui_lmb_to_select"));
    }
    public void TestMethod(int id)
    {
        Debug.Log("Recieved event");
        playerItemIcons[id].HideShowItem(!playerItemIcons[id].isShown);
        cloneAltarButton.UpdateCloneButton(saveData.item1Id, "222");
        cloneAltarButton.ChangeMode(CloneAltarButton.InteractMode.Normal);
    }
    public void TrySelectItem(int itemSlot)
    {
        if (selectedItemSlot == 0)
        {
            SelectItem(itemSlot);
        }
        else
        {
            if(selectedItemSlot == itemSlot)
            {
                TryDeselectItem();
            }
            else
            {
                TryDeselectItem();
                SelectItem(itemSlot);
            }
        }
        UpdateCloneButton();
    }
    private void SelectItem(int itemSlot)
    {
        switch (itemSlot)
        {
            case 1:
                selectedItemId = saveData.item1Id;
                break;
            case 2:
                selectedItemId = saveData.item2Id;
                break;
            case 3:
                selectedItemId = saveData.item3Id;
                break;
        }

        if (selectedItemId == -1) return;

        onAltarItem.UpdateItem(selectedItemId);
        onAltarItem.HideShowItem();
        playerItemIcons[itemSlot - 1].HideShowItem(false);
        selectedItemSlot = itemSlot;
    }
    private void UpdateCloneButton()
    {
        if(selectedItemId >= 0)
        {
            int clonePrice = CalcClonePrice(ItemSpriteDictionary.itemRarity[selectedItemId]);

            cloneAltarButton.UpdateCloneButton(selectedItemId, clonePrice.ToString());

            if(clonePrice <= saveData.moneyAmount && CheckForStorage() >= 0)
            {
                cloneHintText.text = LocalisationSystem.GetLocalizedValue("ui_cloning_room_ready_to_clone");
                cloneAltarButton.ChangeMode(CloneAltarButton.InteractMode.Normal);
            }
            else
            {
                if(CheckForStorage() >= 0)
                    cloneHintText.text = LocalisationSystem.GetLocalizedValue("ui_cloning_room_not_enough_money");
                else
                    cloneHintText.text = LocalisationSystem.GetLocalizedValue("ui_cloning_room_no_room_in_chest");
                cloneAltarButton.ChangeMode(CloneAltarButton.InteractMode.Locked);
            }
        }
        else
        {
            cloneAltarButton.ChangeMode(CloneAltarButton.InteractMode.Hidden);
        }
    }
    public void TryDeselectItem()
    {
        if(selectedItemSlot != 0)
        {
            onAltarItem.HideShowItem(false);
            playerItemIcons[selectedItemSlot - 1].HideShowItem();
            selectedItemSlot = 0;
            selectedItemId = -1;

            cloneHintText.text = LocalisationSystem.GetLocalizedValue("ui_cloning_room_select_item_to_clone");
        }
        UpdateCloneButton();
    }

    public void TryCloneItem()
    {
        int clonePrice = CalcClonePrice(ItemSpriteDictionary.itemRarity[selectedItemId]);

        if (clonePrice <= saveData.moneyAmount && CheckForStorage() >= 0)
        {
            //Visual
            Instantiate(cloneEffectPrefab).GetComponent<OnItemClonedEffect>().InitAndAnim(selectedItemId, animStartTransform, animEndTransform);
            //Logic
            saveData.moneyAmount -= clonePrice;
            saveData.storageChestData.storageItemIds[CheckForStorage()] = selectedItemId;
            Debug.Log("Item cloned");
            UpdateCloneButton();
            UpdateMoneySlotsLabels();
            ApplySaveData();
        }
    }
    private int CalcClonePrice(int itemRarity)
    {
        //CHANGE PRICE FUNTION
        return 200 * (itemRarity + 1);
    }
    private int CheckForStorage()
    {
        for(int i = 0; i < (5 * (2 + saveData.storageChestData.storageChestLevel)); i++)
        {
            Debug.Log($"Checking slot {i}, id: {saveData.storageChestData.storageItemIds[i]}");
            if(saveData.storageChestData.storageItemIds[i] < 0)
            {
                return i;
            }
        }
        return -1;
    }
    private int GetStorageFreeSlots()
    {
        int freeStorageSlots = 0;
        for (int i = 0; i < (5 * (2 + saveData.storageChestData.storageChestLevel)); i++)
        {
            Debug.Log($"Checking slot {i}, id: {saveData.storageChestData.storageItemIds[i]}");
            if (saveData.storageChestData.storageItemIds[i] < 0)
            {
                freeStorageSlots++;
            }
        }
        return freeStorageSlots;
    }
    private void ApplySaveData()
    {
        MenuDataManager.saveData = saveData;
        SaveSystem.Save(MenuDataManager.saveData);
    }
    private void UpdateItemsFromSaveData()
    {
        if (saveData.item1Id >= 0)
            playerItemIcons[0].UpdateItem(saveData.item1Id, 0, LocalisationSystem.GetLocalizedValue("ui_lmb_to_select"));
        else
            playerItemIcons[0].gameObject.SetActive(false);

        if (saveData.item2Id >= 0)
            playerItemIcons[1].UpdateItem(saveData.item2Id, 1, LocalisationSystem.GetLocalizedValue("ui_lmb_to_select"));
        else
            playerItemIcons[1].gameObject.SetActive(false);

        if (saveData.item3Id >= 0)
            playerItemIcons[2].UpdateItem(saveData.item3Id, 2, LocalisationSystem.GetLocalizedValue("ui_lmb_to_select"));
        else
            playerItemIcons[2].gameObject.SetActive(false);
    }
    private void UpdateMoneySlotsLabels()
    {
        playerMoneyLabel.text = saveData.moneyAmount.ToString();
        playerStorageSpaceLabel.text = GetStorageFreeSlots().ToString();
    }
}
